using Crud.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.DAL.Repositories.Contracts
{
    public interface IServicioUsuario
    {
        Task<IEnumerable> Obtener();
        Task<Usuario> Eliminar(string id);
    }
}
