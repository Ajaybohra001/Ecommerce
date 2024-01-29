using Microsoft.AspNetCore.Mvc;
using SuccessiveCart.Data;
using SuccessiveCart.Models.Domain;
using SuccessiveCart.Models.Dto;

namespace SuccessiveCart.Controllers
{
    public class SignUpController : Controller
    {
        private readonly SuccessiveCartDbContext _cartContext;
        public SignUpController(SuccessiveCartDbContext cartContext)
        {
            _cartContext = cartContext;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(_cartContext.Users.Any(u=>u.UserName==model.UserName)) 
                {
                    ModelState.AddModelError("Username", "Username is already taken");
                    return View(model);
                }

                var newUser = new Users()
                {
                    UserName = model.UserName,
                    UserEmail = model.UserEmail,
                    UserPassword = model.UserPassword,
                    UserRole="Public",
                    UserPhoneNo=model.UserPhoneNumber


                };
                _cartContext.Users.Add(newUser);
                _cartContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);

        }
    }
}
