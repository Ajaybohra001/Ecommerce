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
            return View();
        }
        [HttpGet]
        public IActionResult UserDashboard()
        {
            return View();
        
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(AdminViewModel model)
        {
            if(ModelState.IsValid) 
            {
                var user=await _cartContext.Users.FirstOrDefaultAsync(obj=>obj.UserEmail==model.UserEmail && obj.UserPassword==model.UserPassword);

                if (user != null)
                {
                    if (user.UserRole == "Admin")
                        return RedirectToAction("AdminDashboard");

                    else
                        return

                            RedirectToAction("UserDashboard");
                    
                }

                ModelState.AddModelError(string.Empty, "Invalid UserName and Password");
            
            }
            return RedirectToAction("Login");
        }

        

    }
}
