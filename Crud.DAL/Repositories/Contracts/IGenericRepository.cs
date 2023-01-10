using Crud.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        Task<TEntityModel> Insertar(TEntityModel model);

        Task<TEntityModel> Actualizar(TEntityModel model);

        Task<Producto> Eliminar(int id);

        Task<Producto> Obtener(int id);

        Task<IEnumerable<TEntityModel>> ObtenerTodos();

    }
}
