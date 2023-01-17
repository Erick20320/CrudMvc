using Crud.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.BLL.Service.Contracts
{
    public interface IProductoService
    {
        Task<Producto> Insertar(Producto producto);

        Task<Producto> Actualizar(Producto producto);

        Task<Producto> Eliminar(int id);

        Task<Producto> Obtener(int id);

        Task<List<Producto>> ObtenerTodos();

    }
}
