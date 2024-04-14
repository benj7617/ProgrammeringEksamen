using Microsoft.AspNetCore.Mvc;
using Programmering_Eksamen.Data;
using Programmering_Eksamen.Models;
using System.Diagnostics;
using Programmering_Eksamen.Data.Entities;
using Microsoft.EntityFrameworkCore;

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
        [HttpPost]
        public IActionResult Purchase([FromBody] int[] dataArray)
        {
            
            int userId = dataArray[0];

			dataArray = dataArray.Where((source, index) => index != 0).ToArray();


			List<Order> orders = new List<Order>();
			List<OrderProducts> orderProducts = new List<OrderProducts>();
            string TimeAndDate = DateTime.Now.ToString();
			for (int i = 0; i < dataArray.Count(); i++){
                
                Order temp = new Order();
                temp.CreatedDate = TimeAndDate;
                temp.UserID = userId;
				orders.Add(temp);

                
			}
			_context.Orders.AddRange(orders);
			_context.SaveChanges();

			List<int> orderIds = _context.Orders
							.Where(o => o.CreatedDate == TimeAndDate && o.UserID == userId)
							.Select(o => o.Id)
							.ToList();

			for (int i = 0; i < orderIds.Count(); i++)
			{
				OrderProducts temp = new OrderProducts();
                temp.Product = dataArray[i];
				temp.Order = orderIds[i];

		
				orderProducts.Add(temp);
			}

            _context.OrderProducts.AddRange(orderProducts);

			_context.SaveChanges();


            return View();
        }
        public IActionResult ProductList()
        {
			List<Product> productsByCost = _context.Products.OrderBy(p => p.Cost).ToList(); //henter elementerne i products sorteret efter pris
            return View(productsByCost); //sender denne data videre til "ProductList" viewet
        }
        [HttpPost]
        public IActionResult Index2([FromBody] List<Product> products)
        {
            
            _context.Database.EnsureCreated();
          

            if (!_context.Products.Any())
            {
                _context.Products.AddRange(products);
            }
            else
            {
				return RedirectToAction("Index");
			}
            _context.SaveChanges();
            return RedirectToAction("Index");
           
        }

        [HttpPost]
		public IActionResult Search(string searchTerm)
		{
			var products = _context.Products.Where(p => p.Name.Contains(searchTerm)).ToList();
			return Json(products);
		}



	//SIGNUP-Benjamin

	[HttpGet] // Kode til bare at gå ind på siden
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost] // Kode til at submitte indtastet info
        public IActionResult Signup(string username, string password, string email)
        {
            // checker database for om brugernavn er i brug
            if (_context.Users.Any(u => u.Name == username))
            {
                string message = "Username  allerede i brug";
                ViewData["errormsg"] = message;
                return View(); // viser error message
            }
            if (_context.Users.Any(u => u.email == email))
            {
                string emailibrug = "Email allerede i brug";
                ViewData["errormsg"] = emailibrug;
                return View(); // viser error message
            }

            // Skaber ny User med indtastede info
            var user = new User { Name = username, password = password, email = email };

            // Tilføjer bruger til databasen og gemmer det
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login"); // Sender bruger videre til login.cshtml
        }

        /////////////////////////////////////////////////////////////LOGIN-Benjamin/////////////////////////////////////////////////////////////////////////

        [HttpGet] // metoden til bare at gå ind på hjemmesiden
        public IActionResult Login()
        {
            return View(); // Return the login view
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // finder ud af om indtastet username matcher nogen i database
            var user = _context.Users.FirstOrDefault(u => u.Name == username);
            
            if (user != null && user.password == password) //hvis brugernavn findes og password er korrekt
            {
                TempData["username"] = user.Name; //sender username videre
                TempData["password"] = user.password; //sender password videre
                TempData["userID"] = user.Id; //sender userID videre
                //skal bruge tempdata for at dataen faktisk sendes videre, når det ikke bare er return View() men RedirectToAction
                return RedirectToAction("LoginLogic", "Home"); // Sender brugeren til indexsiden efter succesfuld login
            }
            else if(user != null) //hvis bruger findes men password ikke er korrekt
            {
                //gemmer error message til brug på login.cshtml
                string message = "Password er forkert";
                ViewData["errormsg"] = message;
                //sender bruger til opdateret login.cshtml
                return View();
            }
            else 
            {
                string message = "Brugeren ikke fundet";
                ViewData["errormsg"] = message;
                return View(); 
            }
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

		public IActionResult Logout()
		{
			return View();
		}
		public IActionResult Admin()
		{
			return View();
		}


        //Benjamin
        [HttpGet]
        public IActionResult ModifyProduct()
		{
            List<Product> productsById = _context.Products.OrderBy(p => p.Id).ToList(); 
            return View(productsById);
		}

        [HttpPost]
        public IActionResult ModifyProduct(int id, double? price, int? quantity) //både amount og quantity er valgfri
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            Debug.WriteLine(product);
            if (product == null) //checker om produktet findes
            {
                TempData["response"] = "Product Not Found";
                Debug.WriteLine("none found");
                return View();
            }

            if (price.HasValue)
            {
                product.Cost = price.Value;
                TempData["response"] = "Price updated";
                Debug.WriteLine(price.Value);
            }

            if (quantity.HasValue)
            {
                product.Amount = quantity.Value;
                if (TempData["response"] != null)
                {
                    TempData["response"] = $"{TempData["response"]} Quantity updated";
                }
                else
                {
                    TempData["response"] = "Quantity updated";
                }
                Debug.WriteLine(quantity.Value);
            }

            Debug.WriteLine(product);
            _context.SaveChanges();

			List<Product> productsById = _context.Products.OrderBy(p => p.Id).ToList();
			return View(productsById);
		}

        public IActionResult LoginLogic()
        {
            return View();
        }
    }
}
