using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuccessiveCart.Data;
using SuccessiveCart.Models.Domain;

namespace SuccessiveCart.Controllers
{
    public class UserController : Controller
    {
        private readonly SuccessiveCartDbContext _dbContext;
        public async Task<IActionResult> UsersList()
        {
            var users = await _dbContext.Users.ToListAsync();

            return View(users);

        }
        [HttpGet]
        public IActionResult AddUser()

        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(Users model)
        {
            if (model.UserPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Password and Confirm Password must match.");
                return View(model);
            }
            var user = new Users()
            {
                // UserID = model.UserID,
                Name = model.Name,
                UserEmail = model.UserEmail,
                UserPassword = model.UserPassword,
                ConfirmPassword = model.ConfirmPassword,
                UserPhoneNo = model.UserPhoneNo,
                UserRole = model.UserRole
            };


            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return RedirectToAction("UsersList");
        }

        [HttpGet]
        public async Task<IActionResult> ViewUser(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user != null)
            {
                var viewModel = new Users()
                {
                    UserID = user.UserID,
                    Name = user.Name,
                    UserEmail = user.UserEmail,
                    UserPassword = user.UserPassword,
                    UserPhoneNo = user.UserPhoneNo,
                    UserRole = user.UserRole



                };
                return View(viewModel);
            }
            return RedirectToAction("UsersList");
        }
        [HttpPost]
        public async Task<IActionResult> ViewUser(Users model)
        {
            var user = await _dbContext.Users.FindAsync(model.UserID);
            if (user != null)
            {
                user.Name = model.Name;
                user.UserEmail = model.UserEmail;
                user.UserPassword = model.UserPassword;
                user.UserPhoneNo = model.UserPhoneNo;
                user.UserRole = model.UserRole;
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("UsersList");

            }
            return RedirectToAction("UsersList");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Users model)
        {
            var user = await _dbContext.Users.FindAsync(model.UserID);

            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("UsersList");
            }
            return RedirectToAction("UsersList");

        }




    }
}
