using Crud.BLL.Service.Contracts;
using Crud.DAL.Models;
using Crud.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.BLL.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IServicioAutenticacionUsuario _usuarRepo;

        public UsuarioService(IServicioAutenticacionUsuario usuarRepo)
        {
            _usuarRepo = usuarRepo;
        }
        public async Task<Estado> ChangePasswordAsync(CambiarContrasena model, string username)
        {
            try
            {
                return await _usuarRepo.ChangePasswordAsync(model,username);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Estado> LoginAsync(Acceso model)
        {
            try
            {
                return await _usuarRepo.LoginAsync(model);
            }
            catch
            {
                throw;
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _usuarRepo.LogoutAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Estado> RegisterAsync(Registrar model)
        {
            try
            {
                return await _usuarRepo.RegisterAsync(model);
            }
            catch
            {
                throw;
            }
        }
    }
}
