using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuccessiveCart.Data;
using SuccessiveCart.Models.Domain;
using SuccessiveCart.Models.Dto;

namespace SuccessiveCart.Controllers
{
    public class LoginController : Controller
    {
       private readonly SuccessiveCartDbContext _cartContext;


        public LoginController(SuccessiveCartDbContext cartContext)
        {
            _cartContext = cartContext;
            
        }
        [HttpGet]
        public IActionResult AdminDashboard()
        {
            ViewBag.CategoryList = _cartContext.Cateogries.ToList();
            var prod = _cartContext.Products.ToList();

            return View(prod);
        }
        [HttpGet]
        public IActionResult UserDashboard()
        {
            return View();
        
        }

      

        

    }
}
