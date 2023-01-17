using Crud.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.BLL.Service.Contracts
{
    public interface IUsersService
    {
        Task<List<Usuario>> Obtener();
        Task<Usuario> Eliminar(string id);
    }
}
