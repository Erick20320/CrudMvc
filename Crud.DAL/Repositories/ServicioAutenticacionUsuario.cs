using Crud.DAL.Models;
using Crud.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Crud.DAL.Repositories
{
    public class ServicioAutenticacionUsuario : IServicioAutenticacionUsuario
    {
        private readonly UserManager<Usuario> userManager;
       
        private readonly SignInManager<Usuario> signInManager;

        private readonly IServicioEmail _emailService;

        private readonly IConfiguration _configuration;

        public ServicioAutenticacionUsuario(UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager, IServicioEmail emailService, IConfiguration configuration)
        {
            this.userManager = userManager;
            
            this.signInManager = signInManager;

            _emailService = emailService;

            _configuration = configuration;

        }

        public async Task<Estado> LoginAsync(Acceso model)
        {
            var status = new Estado();
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Nombre de usuario no válido";
                return status;
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Contraseña invalida";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Inicio sesión con éxito";
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "El usuario fue bloqueado";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error al iniciar sesión";
            }

            return status;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<Estado> RegisterAsync(Registrar model)
        {
            var status = new Estado();
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "El usuario ya existe";
                return status;
            }
            Usuario user = new Usuario()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,

            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await GenerateEmailConfirmationTokenAsync(user);
            }
            return status;
        }

        public async Task<Usuario> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task GenerateEmailConfirmationTokenAsync(Usuario user)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(user, token);
            }
        }

        public async Task GenerateForgotPasswordTokenAsync(Usuario user)
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await userManager.ConfirmEmailAsync(await userManager.FindByIdAsync(uid), token);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetearContrasena model)
        {
            return await userManager.ResetPasswordAsync(await userManager.FindByIdAsync(model.UserId), model.Token, model.NewPassword);
        }

        private async Task SendEmailConfirmationEmail(Usuario user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            UsuarioEmailOpciones options = new UsuarioEmailOpciones
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailForEmailConfirmation(options);
        }

        private async Task SendForgotPasswordEmail(Usuario user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;

            UsuarioEmailOpciones options = new UsuarioEmailOpciones
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailForForgotPassword(options);
        }

    }
}
