using Crud.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.DAL.Repositories.Contracts
{
    public interface IServicioAutenticacionUsuario
    {
        Task<Estado> LoginAsync(Acceso model);
        Task LogoutAsync();
        Task<Estado> RegisterAsync(Registrar model);
        Task<IdentityResult> ResetPasswordAsync(ResetearContrasena model);

        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task GenerateEmailConfirmationTokenAsync(Usuario user);
        Task GenerateForgotPasswordTokenAsync(Usuario user);
        Task<Usuario> GetUserByEmailAsync(string email);

    }
}
