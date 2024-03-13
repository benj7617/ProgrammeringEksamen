using Microsoft.AspNetCore.Mvc;
using Programmering_Eksamen.Data;
using Programmering_Eksamen.Models;
using System.Diagnostics;
using Programmering_Eksamen.Data.Entities;

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
        public IActionResult Login()
        {
            return View();
        }




        [HttpGet] //standard, som alle bruger per default. skal skrives her grundet vi har 2 forkellige
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost] //tillader at der bliver sendt data fra frontend til databasen
        public IActionResult AddProduct(Product product) //har 2 af samme navn, polymorfi betyder vi kan have 2 med samme navn, som bruges alt efter input argumenter
        {
            _context.Products.Add(product); //tilføjer til databasen
            _context.SaveChanges(); //gemme det nye objekt til databsen
            return View(); //kunne ændres til en redirect så det bliver tydeligere for brugeren at der er sket noget
        }


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
