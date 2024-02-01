using Microsoft.AspNetCore.Identity;
using SuccessiveCart.Constant;
using SuccessiveCart.Models.Domain;
using System.Data;

namespace SuccessiveCart.Data
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<Users>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            // creating admin

            var user = new Users
            {
                UserName = "ajay@gmail.com",
                Name="Ajay",
                UserRole="Admin",
               
                UserEmail = "ajay@gmail.com",
                UserPassword="ajaybohra7",
                ConfirmPassword="ajaybohra7",
                UserPhoneNo="8810304137",
                
            };
            var userInDb = await userManager.FindByEmailAsync(user.UserEmail);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }
    }
}

