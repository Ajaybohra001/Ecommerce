using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SuccessiveCart.Models.Domain;
using SuccessiveCart.Data;
using Microsoft.EntityFrameworkCore;
using SuccessiveCart.Models.Dto;

namespace SuccessiveCart.Controllers
{
    public class Favourite : Controller
    {
        private readonly SuccessiveCartDbContext _context;
        private readonly UserManager<Users> _userManager;

        public Favourite(SuccessiveCartDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

      

        public IActionResult FavouriteProducts()
        {
            var userId = _userManager.GetUserId(User);

            var FavouriteWithProducts = _context.favorites
                .Where(ci => ci.UserId.ToString() == userId)
                .Join(
                    _context.Products,
                    favouriteItem => favouriteItem.ProductId,
                    product => product.ProductId,
                    (favouriteItem, product) => new FavouriteWithProducts
                    {
                        FavId = favouriteItem.FavId,
                        ProductId = favouriteItem.ProductId,
                        
                        ProductName = product.ProductName,
                        ProductPrice = product.ProductPrice,
                        ProductPhoto = product.ProductPhoto,
                        ProductDescription = product.ProductDescription,
                        IsAvailable = product.IsAvailable,
                        IsTrending = product.IsTrending,
                        ProductCreatedDate = product.ProductCreatedDate
                    }
                )
                .ToList();

            return View(FavouriteWithProducts);
        }

           
        [HttpPost]
        public async Task<IActionResult> AddToFavourite(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            if (user == null)
            {
                return NotFound("User not found.");
            }


            if (product != null)
            {
                var userId = _userManager.GetUserId(User);

             
                var existingFavorite = await _context.favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == id);
               

                if (existingFavorite != null)
                {
                    _context.favorites.Remove(existingFavorite);
                }
                else
                {
                    
                    _context.favorites.Add(new FavoriteModel
                    {
                        FavId=Guid.NewGuid(),
                        UserId = userId,
                        ProductId = id,
                        Users=user,
                        Products=product
                        
                    });
                }

                await _context.SaveChangesAsync();
            }

            
            return RedirectToAction("FavouriteProducts"); 
        }
    }
}
