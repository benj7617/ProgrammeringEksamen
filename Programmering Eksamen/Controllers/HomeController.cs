using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Programmering_Eksamen.Data;
using Programmering_Eksamen.Data.Entities;
using Programmering_Eksamen.Models;

namespace Programmering_Eksamen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly connectionDBContext _context; //connecter controlleren til vores backend/database

        public HomeController(ILogger<HomeController> logger, connectionDBContext context)
        {
            _logger = logger;
            _context = context; //skaber en variabel der kan bruges til at lave calls til backend
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult ProductList()
        {
            List<Product> productsByCost = _context.Products.OrderBy(p => p.Cost).ToList(); //henter elementerne i products sorteret efter pris
            return View(productsByCost); //sender denne data videre til "ProductList" viewet
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
