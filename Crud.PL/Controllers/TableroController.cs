using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crud.PL.Controllers
{
    [Authorize]
    public class TableroController : Controller
    {
        public IActionResult Mostrar()
        {
            return View();
        }
    }
}
