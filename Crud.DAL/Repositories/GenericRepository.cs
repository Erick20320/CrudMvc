using Crud.DAL.DataContext;
using Crud.DAL.Models;
using Crud.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.DAL.Repositories
{
    public class GenericRepository<TEntityModel> : IGenericRepository<TEntityModel> where TEntityModel : class
    {
        private readonly CrudContext _crudcontext;

        public GenericRepository(CrudContext context)
        {
            _crudcontext = context;
        }

        public async Task<TEntityModel> Actualizar(TEntityModel model)
        {
            try
            {
                _crudcontext.Update(model);
                _crudcontext.SaveChanges();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Producto> Eliminar(int id)
        {
            try
            {
                Producto p = await _crudcontext.Productos.FirstOrDefaultAsync(m => m.ProductoId == id);

                if (p != null)
                {
                    _crudcontext.Productos.Remove(p);
                    _crudcontext.SaveChanges();
                }

                return p;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TEntityModel> Insertar(TEntityModel model)
        {
            try
            {
                _crudcontext.Add(model);
                _crudcontext.SaveChanges();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Producto> Obtener(int id)
        {
            try
            {
                return await _crudcontext.Productos.FirstOrDefaultAsync(m => m.ProductoId == id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<TEntityModel>> ObtenerTodos()
        {
            try
            {
                await _crudcontext.SaveChangesAsync();

                return await _crudcontext.Set<TEntityModel>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
