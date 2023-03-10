using Crud.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.BLL.Service.Contracts
{
    public interface IUsuarioService
    {
        Task<Estado> LoginAsync(Acceso model);
        Task LogoutAsync();
        Task<Estado> RegisterAsync(Registrar model);
        Task<IdentityResult> ResetPasswordAsync(ResetearContrasena model);
    }
}
