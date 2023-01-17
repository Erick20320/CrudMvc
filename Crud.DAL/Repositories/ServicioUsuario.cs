using Crud.DAL.DataContext;
using Crud.DAL.Models;
using Crud.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.DAL.Repositories
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly CrudContext _crudcontext;

        public ServicioUsuario(CrudContext context)
        {
            _crudcontext = context;
        }

        public async Task<Usuario> Eliminar(string id)
        {
            try
            {
                Usuario u = await _crudcontext.Users.FirstOrDefaultAsync(m => m.Id == id);

                if (u != null)
                {
                    _crudcontext.Users.Remove(u);
                    _crudcontext.SaveChanges();
                }

                return u;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable> Obtener()
        {
            try
            {
                await _crudcontext.SaveChangesAsync();
                return _crudcontext.Users.Where(x => x.EmailConfirmed == true).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}
