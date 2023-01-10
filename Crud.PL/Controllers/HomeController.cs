using Crud.BLL.Service;
using Crud.BLL.Service.Contracts;
using Crud.DAL.Models;
using Crud.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Crud.PL.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}