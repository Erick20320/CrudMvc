using Crud.DAL.Models;
using Crud.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Crud.DAL.Repositories
{
    public class ServicioAutenticacionUsuario : IServicioAutenticacionUsuario
    {
        private readonly UserManager<Usuario> userManager;
       
        private readonly SignInManager<Usuario> signInManager;
        public ServicioAutenticacionUsuario(UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager)
        {
            this.userManager = userManager;
            
            this.signInManager = signInManager;

        }

        public async Task<Estado> ChangePasswordAsync(CambiarContrasena model, string username)
        {
            var status = new Estado();

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                status.Message = "El usuario no existe";
                status.StatusCode = 0;
                return status;
            }
            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                status.Message = "La contraseña se ha actualizado correctamente";
                status.StatusCode = 1;
            }
            else
            {
                status.Message = "Ocurrió algún error";
                status.StatusCode = 0;
            }
            return status;
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
                status.Message = "El usuario fue eliminado";
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
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "La creación del usuario falló";
                return status;
            }

            status.StatusCode = 1;
            status.Message = "Te has registrado exitosamente";
            return status;
        }
    }
}
