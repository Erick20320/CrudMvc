using Crud.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crud.DAL.Models;

namespace Crud.PL.Controllers
{
    public class AutenticacionController : Controller
    {
        private readonly IServicioAutenticacionUsuario _authService;
        public AutenticacionController(IServicioAutenticacionUsuario authService)
        {
            this._authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Acceso model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(Registrar model)
        {
            if (ModelState.IsValid)
            {
                // write your code
                var result = await _authService.RegisterAsync(model);


                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new { email = model.Email });
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            ConfirmarEmail model = new ConfirmarEmail
            {
                Email = email
            };

            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result = await _authService.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }

            return View(model);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmarEmail model)
        {
            var user = await _authService.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    await _authService.GenerateEmailConfirmationTokenAsync(user);
                    model.EmailVerified = true;
                    return View(model);
                }

                
                model.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Algo salió mal.");
            }
            return View(model);
        }

        [AllowAnonymous, HttpGet("fotgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("fotgot-password")]
        public async Task<IActionResult> ForgotPassword(OlvideContrasena model)
        {
            if (ModelState.IsValid)
            {
                // code here
                var user = await _authService.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    await _authService.GenerateForgotPasswordTokenAsync(user);
                }

                ModelState.Clear();
                model.EmailSent = true;
            }
            return View(model);
        }


        [Authorize]
        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetearContrasena resetPassword = new ResetearContrasena
            {
                Token = token,
                UserId = uid
            };
            return View(resetPassword);
        }

        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetearContrasena model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result = await _authService.ResetPasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
