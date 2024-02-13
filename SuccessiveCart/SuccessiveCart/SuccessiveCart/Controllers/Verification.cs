using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SuccessiveCart.Constant;
using SuccessiveCart.Data;
using SuccessiveCart.Models.Domain;
using SuccessiveCart.Models.Dto;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using SuccessiveCart.Service;



namespace SuccessiveCart.Controllers
{
    public class Verification : Controller
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SuccessiveCartDbContext _dbContext;
        private readonly Email _email;


        public Verification(SignInManager<Users> signInManager, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager,SuccessiveCartDbContext dbContext,Email email)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext= dbContext;
            _email=email;
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
                bool checkActive =_userManager.Users.FirstOrDefault(c=>c.UserName==model.UserName).isActive;
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
           

            return RedirectToAction("UserDashboard","Login");
        }

        [HttpGet]

        public async Task<IActionResult> GetAllUsers()
        {

           
                var users = await _dbContext.Users.ToListAsync();

                return View(users);

           
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserEmail == email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                  
                    var callbackUrl = Url.Action("ResetPassword", "Verification", new { token = token, email = user.UserEmail }, HttpContext.Request.Scheme);

                    await _email.SendEmail(user.UserEmail, "Reset Password",
                        $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

                }
                return RedirectToAction("ForgotPasswordConfirmation", "Verification");

            }
            return View(email);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                return RedirectToAction("ResetPasswordError", "Verification");
            }

            var model = new ResetPasswordViewModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.UserEmail==model.Email);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordError", "Verification");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
         
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Verification");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordError()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }







    }
}
