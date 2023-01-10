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
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _productRepo;

        public ProductoService(IGenericRepository<Producto>productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<Producto> Actualizar(Producto producto)
        {
            try
            {
                return await _productRepo.Actualizar(producto);
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
                return await _productRepo.Eliminar(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Producto> Insertar(Producto producto)
        {
            try
            {
                return await _productRepo.Insertar(producto);
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
                return await _productRepo.Obtener(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Producto>> ObtenerTodos()
        {
            try
            {
                return (List<Producto>)await _productRepo.ObtenerTodos();
            }
            catch
            {
                throw;
            }
        }
    }
}
