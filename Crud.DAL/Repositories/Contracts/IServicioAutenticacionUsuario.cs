using Crud.DAL.Models;
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
        Task<Estado> ChangePasswordAsync(CambiarContrasena model, string username);
    }
}
