using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuccessiveCart.Models;
using SuccessiveCart.Data;
using System.Diagnostics;
using System.Xml.Linq;

namespace SuccessiveCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SuccessiveCartDbContext _context;

        public HomeController(ILogger<HomeController> logger, SuccessiveCartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ProductList = _context.Products.ToList();
            ViewBag.CategoryList = _context.Cateogries.ToList();

            var prod = _context.Products.ToList();
            return View(prod);
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
