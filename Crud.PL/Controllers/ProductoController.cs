using Crud.BLL.Service.Contracts;
using Crud.DAL.DataContext;
using Crud.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud.PL.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoServ)
        {
            _productoService = productoServ;
        }

        [HttpGet]
        public async Task<IActionResult> ListaProductos()
        {
            List<Producto> listProductos = await _productoService.ObtenerTodos();
            return View(listProductos);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] Producto producto)
        {
            await _productoService.Insertar(producto);
            return Redirect("ListaProductos");
        }

        public IActionResult InsertarProd()
        {
            return View("ListaProductos");
        }

        [HttpPost]

        public async Task<IActionResult> Actualizar([FromForm] Producto producto)
        {
            await _productoService.Actualizar(producto);
            return Redirect("ListaProductos");
        }

        [HttpGet]
        public async Task<IActionResult> Obtener(int id)
        {
            return View("EditarProductos", await _productoService.Obtener(id));
        }


        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            return View("View", await _productoService.Eliminar(id));
        }

        public async Task<IActionResult> EliminarProd(int id)
        {
            await _productoService.Eliminar(id);
            return Redirect("~/Producto/ListaProductos");
        }
    }
}
