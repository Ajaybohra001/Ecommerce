using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuccessiveCart.Data;
using SuccessiveCart.Models.Domain;
using SuccessiveCart.Models.Dto;
using System.Reflection.Metadata.Ecma335;

namespace SuccessiveCart.Controllers
{
    public class AdminController : Controller
    {
        private readonly SuccessiveCartDbContext _dbContext;
        private readonly IWebHostEnvironment WebHostEnvironment;

        public AdminController(SuccessiveCartDbContext dbContext,IWebHostEnvironment webHostEnvironment )
        {
            _dbContext = dbContext;
            this.WebHostEnvironment= webHostEnvironment;
            _dbContext.Products.Include(u=>u.Cateogries);
            
        }
       

        
        public IActionResult Products()
        {
            var products = _dbContext.Products.ToList();
            return View(products);
        }

       

        [HttpGet]
        public IActionResult AddProducts()
        {
            var cateogryList = _dbContext.Cateogries.ToList();
            ViewBag.CateogryList = cateogryList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProducts(ProductViewModel model)
        {
            string uniqueFileName = "";
            if ( model.ProductPhoto!= null)
            {
                string uploadFolder = Path.Combine(WebHostEnvironment.WebRootPath, "image");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductPhoto.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                model.ProductPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            var newProduct = new Products()
            {

                ProductName = model.ProductName,
                ProductPrice = model.ProductPrice,
                ProductPhoto = uniqueFileName,
                ProductDescription = model.ProductDescription,
                ProductCreatedDate = DateTime.Now,
                IsAvailable=model.IsAvailable,
                IsTrending=model.IsTrending,
                CateogryId=model.CateogryId
               // ProductId=model.ProductId,
                //Cateogry=model.Cateogry,
               
                


            };
            await _dbContext.Products.AddAsync(newProduct);
            await _dbContext.SaveChangesAsync();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("AdminDashboard" , "Login");
        
        }
        [HttpGet]
        public async Task<IActionResult> ViewProduct(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p=>p.ProductId== id);
            ViewBag.CategoryList=await _dbContext.Cateogries.ToListAsync();
            if (product != null)
            {
                var viewModel = new ProductViewModel()
                {
                    ProductId=product.ProductId,
                    ProductName=product.ProductName,
                 //ProductPhoto=product.ProductPhoto,
                 ProductCreatedDate= product.ProductCreatedDate,
                 ProductDescription = product.ProductDescription,
                 ProductPrice=product.ProductPrice,
                 CateogryId=product.CateogryId,
                 IsAvailable= product.IsAvailable,
                 IsTrending = product.IsTrending


                 



                };
                return await Task.Run(()=> View("ViewProduct",viewModel));
            }
            return RedirectToAction("AdminDashboard", "Login");
        }
        [HttpPost]
        public async Task<IActionResult> ViewProduct(ProductViewModel model)
        {
            var product = await _dbContext.Products.FindAsync(model.ProductId);
            ViewBag.CategoryList=_dbContext.Cateogries.ToList();
            string uniqueFileName = "";
            if (model.ProductPhoto != null)
            {
                string uploadFoler = Path.Combine(WebHostEnvironment.WebRootPath, "image");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductPhoto.FileName;
                string filePath = Path.Combine(uploadFoler, uniqueFileName);
                model.ProductPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            if (product != null)
            {
                product.ProductName = model.ProductName;
                product.ProductPhoto = uniqueFileName;
                product.ProductPrice = model.ProductPrice;
                product.ProductDescription = model.ProductDescription;
                product.CateogryId = model.CateogryId;
                product.IsAvailable = model.IsAvailable;
                product.IsTrending = model.IsTrending;
                product.ProductCreatedDate = model.ProductCreatedDate;
               
               


            }
            await _dbContext.SaveChangesAsync();
            TempData["success"] = "Product Updated successfully";

            return RedirectToAction("AdminDashboard","Login");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Products model)
        {
            var product = await _dbContext.Products.FindAsync(model.ProductId);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Product Deleted successfully";

                return RedirectToAction("Products");
            }
            return RedirectToAction("Products");

        }


        public async  Task<IActionResult> UsersList() 
        { var users=await _dbContext.Users.ToListAsync();
           
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
            var user = new Users()
            {
               // UserID = model.UserID,
                UserName = model.UserName,
                UserEmail = model.UserEmail,
                UserPassword = model.UserPassword,
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
                { UserID=user.UserID,
                   UserName= user.UserName,
                   UserEmail= user.UserEmail,
                   UserPassword= user.UserPassword,
                   UserPhoneNo= user.UserPhoneNo,
                   UserRole= user.UserRole



                };
                return View(viewModel);
            }
            return RedirectToAction("UsersList");
        }
        [HttpPost]
        public async Task<IActionResult> ViewUser(Users model)
        {
            var user = await _dbContext.Users.FindAsync(model.UserID);
            if(user!=null) 
            {
                user.UserName=model.UserName;
                user.UserEmail= model.UserEmail;
                user.UserPassword=model.UserPassword;
                user.UserPhoneNo= model.UserPhoneNo;
                user.UserRole= model.UserRole;
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("UsersList");
            
            }
            return RedirectToAction("UsersList");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Users model)
        {
            var user = await _dbContext.Users.FindAsync(model.UserID);

            if (user!= null)
            {
               _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("UsersList");
            }
            return RedirectToAction("UsersList");
        
    }

        public async Task<IActionResult> CateogryList()
        {
            var cateogry = await _dbContext.Cateogries.ToListAsync();

            return View(cateogry);
        }

        [HttpGet]
        public IActionResult AddCateogry()

        {


            return View();
        }
        [HttpPost]
        public IActionResult AddCateogry(Cateogry model)
        {
            //string uniqueFileName = "";
            //if (model.Product != null)
            //{
            //    string uploadFolder = Path.Combine(WebHostEnvironment.WebRootPath, "image");
            //    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductPhoto.FileName;
            //    string filePath = Path.Combine(uploadFolder, uniqueFileName);
            //    model.ProductPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
            //}
            var cateogry = new Cateogry()
            {
                CateogryName=model.CateogryName,
                CateogryPhoto=model.CateogryPhoto,
             
            };
            _dbContext.Cateogries.Add(cateogry);
            _dbContext.SaveChanges();
            return RedirectToAction("CateogryList");
        }

        [HttpGet]
        public async Task<IActionResult> ViewCateogry(int id)
        {
            var cateogry = await _dbContext.Cateogries.FirstOrDefaultAsync(c=>c.CateogryId== id);
            if (cateogry != null)
            {
                var viewModel = new Cateogry()
                {
                   CateogryId=cateogry.CateogryId,
                   CateogryName=cateogry.CateogryName,
                   CateogryPhoto=cateogry.CateogryPhoto

                   



                };
                return View(viewModel);
            }
            return RedirectToAction("CateogryList");
        }


        [HttpPost]
        public async Task<IActionResult> ViewCateogry(Cateogry model)
        {
            var cateogry =await  _dbContext.Cateogries.FindAsync(model.CateogryId);
            if(cateogry!=null)
            {
               cateogry.CateogryName=model.CateogryName;
                cateogry.CateogryPhoto= model.CateogryPhoto;
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("CateogryList");
            }
            return RedirectToAction("CateogryList");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCateogry(Cateogry model)
        {
            var cateogry = await _dbContext.Cateogries.FindAsync(model.CateogryId);

            if (cateogry != null)
            {
                _dbContext.Cateogries.Remove(cateogry);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("CateogryList");
            }
            return RedirectToAction("CateogryList");

        }

        #region API CALLS
        [HttpGet]
        public async Task< IActionResult> GetAll()
        {
            var allProducts = _dbContext.Products.ToList();
            var productList = await _dbContext.Products.ToListAsync();
            var categoryList = await _dbContext.Cateogries.ToListAsync();
            var categoryProduct = productList.Join(// outer sequence 
                       categoryList,  // inner sequence 
                       product => product.CateogryId,   // outerKeySelector
                       category => category.CateogryId, // innerKeySelector
                       (product, category) => new ProductCateogry // result selector
                       {
                           ProductId = product.ProductId,
                           ProductName = product.ProductName,
                           ProductDescription = product.ProductDescription,
                           ProductPrice = product.ProductPrice,
                           ProductPhoto = product.ProductPhoto,
                           IsAvailable = product.IsAvailable,
                          
                           ProductCreatedDate = product.ProductCreatedDate,
                           IsTrending = product.IsTrending,
                           CateogryId = category.CateogryId,
                           CateogryName = category.CateogryName
                       }).OrderByDescending(x => x.ProductCreatedDate).ToList();
            //var products = _dbContext.Products.ToList().OrderByDescending(x => x.ProductCreatedDate);
            return Json(new { data = categoryProduct });
        }

        #endregion





    }
}
