using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace SS.Models
{
    public static class InitData
    {
        public static async Task Initialize(MyContext context,UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            if (!context.Brands.Any())
            {
                await context.Brands.AddRangeAsync(
                    new Brand
                    {
                        Name = "Adidas"
                    },
                    new Brand
                    {
                        Name = "Nike"
                    },
                    new Brand
                    {
                        Name = "Puma"
                    },
                    new Brand 
                    {
                        Name = "New Balance"
                    },
                    new Brand 
                    {
                        Name = "Asics"
                    }
                    );
            }
            var brands = context.Brands.Local.ToList();
            if (!context.Sneakers.Any())
            {
                await context.Sneakers.AddRangeAsync(
                    new Sneaker
                    {
                        Name = "SuperCourt Rx",
                        Brand = brands[0],
                        Color = "White",
                        Price = 100,
                        Count = 5,
                        PicturePath = "AdidasSCRX.jpg"
                    },
                    new Sneaker
                    {
                        Name = "SuperCourt",
                        Brand = brands[0],
                        Color = "White",
                        Price = 75,
                        Count = 10,
                        PicturePath = "AdidasSC.jpg"
                    },
                    new Sneaker
                    {
                        Name = "P-6000",
                        Brand = brands[1],
                        Color = "Black",
                        Price = 110,
                        Count = 15,
                        PicturePath = "NIKEP6000.jpg"
                    }
                );
                context.SaveChanges();
            }

            string adminL = "admin";
            string password = "Odin23*";

            if (await roleManager.FindByNameAsync("admin") == null)
                await roleManager.CreateAsync(new IdentityRole("admin"));

            if (await roleManager.FindByNameAsync("user") == null)
                await roleManager.CreateAsync(new IdentityRole("user"));

            if (await userManager.FindByNameAsync(adminL) == null)
            {
                var admin = new User { UserName = adminL,Role = "admin",Email="admin@gmail.com",EmailConfirmed=true };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
