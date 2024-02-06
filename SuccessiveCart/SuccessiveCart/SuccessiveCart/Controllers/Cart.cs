using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuccessiveCart.Data;
using SuccessiveCart.Models.Domain;
using SuccessiveCart.Models.Dto;

namespace SuccessiveCart.Controllers
{
    public class Cart : Controller

    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;

        private readonly SuccessiveCartDbContext _context; 

        public Cart(SuccessiveCartDbContext context,SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            _context = context;
            _signInManager=signInManager;
            _userManager=userManager;
        }

        public IActionResult AllProducts()
        {
            var Products=_context.Products.ToList();
            ViewBag.ProductList = _context.Products.ToList();
            ViewBag.CategoryList = _context.Cateogries.ToList();

            

            return View(Products);
        }

        
        public IActionResult MyCart()
        {
            var userId = _userManager.GetUserId(User);

            var cartItemsWithProducts = _context.CartItems
                .Where(ci => ci.UserId.ToString() == userId)
                .Join(
                    _context.Products,
                    cartItem => cartItem.ProductId,
                    product => product.ProductId,
                    (cartItem, product) => new CartWithProducts
                    {
                        CartId = cartItem.CartId,
                        ProductId = cartItem.ProductId,
                        ProductQuantity = cartItem.ProductQuantity,
                        ProductName = product.ProductName,
                        ProductPrice = product.ProductPrice*cartItem.ProductQuantity,
                        ProductPhoto = product.ProductPhoto,
                        ProductDescription = product.ProductDescription,
                        IsAvailable = product.IsAvailable,
                        IsTrending = product.IsTrending,
                        ProductCreatedDate = product.ProductCreatedDate
                    }
                )
                .ToList();

            return View(cartItemsWithProducts);
        }



        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {


            var product = _context.Products.Find(id);

            if (product == null)
            {

                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var existingCartItem = await _context.CartItems
        .FirstOrDefaultAsync(ci => ci.UserId.ToString()== user.Id && ci.ProductId == id);

            if (existingCartItem != null)
            {
                existingCartItem.ProductQuantity++;
            }

            else
            {
                var cartItem = new CartItem()
                {
                    CartId = Guid.NewGuid(),
                    ProductId = product.ProductId,
                    ProductQuantity = 1,
                    UserId = Guid.Parse(user.Id),
                    Users = user,
                    Products = product




                };


                _context.CartItems.Add(cartItem);
            }
            _context.SaveChanges();


            return RedirectToAction("MyCart");


        }

        [HttpPost]
        public IActionResult RemoveProductCart([FromRoute]Guid id)
        {
            if(id!=null)
            {

                var product = _context.CartItems.Find(id);

                if(product != null)
                {
                    _context.CartItems.Remove(product);
                }
                

            }
            _context.SaveChanges();
            return RedirectToAction("MyCart");


        }




    }
}
