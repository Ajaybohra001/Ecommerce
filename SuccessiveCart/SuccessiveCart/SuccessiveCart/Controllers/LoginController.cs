using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="Admin")]
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
            ViewBag.ProductList = _cartContext.Products.ToList();
            ViewBag.CategoryList = _cartContext.Cateogries.ToList();

            var prod = _cartContext.Products.ToList();
            return View(prod);
        
        }

      

        

    }
}
