using Crud.BLL.Service.Contracts;
using Crud.DAL.Models;
using Crud.DAL.Repositories.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.BLL.Service
{
    public class UsersService : IUsersService
    {
        private readonly IServicioUsuario _usuarRepo;

        public UsersService(IServicioUsuario usuarRepo)
        {
            _usuarRepo = usuarRepo;
        }
        public async Task<Usuario> Eliminar(string id)
        {
            try
            {
                return await _usuarRepo.Eliminar(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Usuario>> Obtener()
        {
            try
            {
                return (List<Usuario>)await _usuarRepo.Obtener();
            }
            catch
            {
                throw;
            }
        }
    }
}
