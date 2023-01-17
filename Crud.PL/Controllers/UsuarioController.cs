using Crud.BLL.Service;
using Crud.BLL.Service.Contracts;
using Crud.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud.PL.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IUsersService _usersService;

        public UsuarioController(IUsersService usersServ)
        {
            _usersService = usersServ;
        }

        [HttpGet]
        public async Task<IActionResult> ListaUsuarios()
        {

            List<Usuario> listUsuarios = await _usersService.Obtener();
            return View(listUsuarios);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar(string id)
        {
            return View("View", await _usersService.Eliminar(id));
        }

        public async Task<IActionResult> EliminarUsuar(string id)
        {
            await _usersService.Eliminar(id);
            return Redirect("~/Usuario/ListaUsuarios");
        }
    }
}
