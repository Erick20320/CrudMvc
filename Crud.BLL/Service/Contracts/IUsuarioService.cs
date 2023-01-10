using Crud.DAL.Models;
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
        Task<Estado> ChangePasswordAsync(CambiarContrasena model, string username);
    }
}
