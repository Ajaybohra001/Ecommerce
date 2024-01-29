using Microsoft.EntityFrameworkCore;
using SuccessiveCart.Models.Domain;

namespace SuccessiveCart.Data
{
    public class SuccessiveCartDbContext:DbContext
    {
        public SuccessiveCartDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Cateogry> Cateogries { get;set; }
        public DbSet<CartItem> CartItems { get; set; }

    }
}
