using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuccessiveCart.Data;
using SuccessiveCart.Models.Domain;
using SuccessiveCart.Models.Dto;
namespace SuccessiveCart.Controllers
{
    [Authorize]

    public class UserProfileController : Controller
    {
       
        private readonly SuccessiveCartDbContext _dbContext;
        public UserProfileController(SuccessiveCartDbContext dbContext)
        {
            
            _dbContext = dbContext;
        }

        public IActionResult Index(string id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                var viewModel = new Users()
                {
                    Name = user.Name,
                    UserEmail = user.UserEmail,
                    UserPassword = user.UserPassword,
                    UserPhoneNo = user.UserPhoneNo,
                    UserRole = user.UserRole,
                    isActive = user.isActive


                };
                return View(viewModel);

            }
            else
                return NotFound();
        }
    }
}

