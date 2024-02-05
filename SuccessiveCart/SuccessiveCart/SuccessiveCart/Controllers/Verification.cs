using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuccessiveCart.Constant;
using SuccessiveCart.Data;
using SuccessiveCart.Models.Domain;
using SuccessiveCart.Models.Dto;

namespace SuccessiveCart.Controllers
{
    public class Verification : Controller
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SuccessiveCartDbContext _dbContext;

        public Verification(SignInManager<Users> signInManager, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager,SuccessiveCartDbContext dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext= dbContext;
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
                bool checkActive = _userManager.Users.FirstOrDefault(c=>c.UserName==model.UserName).isActive;
                if (checkActive)
                {
                    //login
                    var result = await _signInManager.PasswordSignInAsync(model.UserName!, model.Password!, model.RememberMe, false);

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
                else
                {
                    ModelState.AddModelError("", "Your account is not active.");
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
                    isActive=true
                    

                };

                var result = await _userManager.CreateAsync(user, model.UserPassword!);

                if (result.Succeeded)
                {
                   

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

        [HttpGet]

        public async Task<IActionResult> GetAllUsers()
        {

           
                var users = await _dbContext.Users.ToListAsync();

                return View(users);

           
        }


    }
}
