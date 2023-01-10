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
            if (!ModelState.IsValid) { return View(model); }
           
            var result = await this._authService.RegisterAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(CambiarContrasena model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.ChangePasswordAsync(model, User.Identity.Name);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(ChangePassword));
        }
    }
}
