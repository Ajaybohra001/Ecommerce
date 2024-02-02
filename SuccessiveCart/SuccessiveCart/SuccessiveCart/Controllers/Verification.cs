using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuccessiveCart.Constant;
using SuccessiveCart.Models.Domain;
using SuccessiveCart.Models.Dto;

namespace SuccessiveCart.Controllers
{
    public class Verification : Controller
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Verification(SignInManager<Users> signInManager, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {

            if (_signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("AdminDashboard", "Login");
                }
                if (User.IsInRole("User") )
                {
                    return RedirectToAction("UserDashboard", "Login");
                }
            }
          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
           
                if (ModelState.IsValid)
                {
                bool checkActive = _userManager.Users.First(x => x.UserEmail == model.UserEmail).isActive;
                if (checkActive)
                {
                    //login
                    var result = await _signInManager.PasswordSignInAsync(model.UserEmail!, model.Password!, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        if (User.IsInRole("Admin"))
                        {
                            return RedirectToAction("AdminDashboard", "Login");
                        }
                        if (User.IsInRole("User"))
                        {
                            return RedirectToAction("UserDashboard", "Login");
                        }
                    }

                    ModelState.AddModelError("", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Users
                {
                    Name= model.Name,
                    UserName=model.UserEmail,
                   
                    UserEmail = model.UserEmail,
                    
                    UserPhoneNo = model.UserPhoneNo,
                   
                    UserPassword = model.UserPassword,
                    ConfirmPassword = model.ConfirmPassword,
                    UserRole="User",
                    

                };

                var result = await _userManager.CreateAsync(user, model.UserPassword!);

                if (result.Succeeded)
                {
                    if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("UsersList", "Admin");
                    }

                    await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("UserDashboard", "Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

     
       


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
           

            return RedirectToAction("Login");
        }


    }
}
