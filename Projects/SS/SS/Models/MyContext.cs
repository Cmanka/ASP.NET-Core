using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SS.Models
{
    public class MyContext : IdentityDbContext<User>
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Sneaker> Sneakers { get; set; }
        public DbSet<CartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
