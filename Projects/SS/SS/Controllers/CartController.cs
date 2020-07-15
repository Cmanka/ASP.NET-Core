using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using SS.ViewModels;
using SS.Models;
using Microsoft.EntityFrameworkCore;

namespace SS.Controllers
{
    
    public class CartController : Controller
    {
        MyContext context;
        ShoppingCart shoppingCart;
        public CartController(MyContext context,ShoppingCart shoppingCart)
        {
            this.context = context;
            this.shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            shoppingCart.ShoppingCartItems = shoppingCart.GetShoppingCartItems();

            var scvm = new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetTotal(),
                Brands = context.Brands.ToList()
            };

            return View(scvm);
        }

        public async Task<RedirectToActionResult> AddToShopCart(int sneakersId)
        {
            var selectedDrink = await context.Sneakers.FirstOrDefaultAsync(s => s.Id == sneakersId);
            if (selectedDrink != null)
                shoppingCart.AddToCart(selectedDrink, 1);
            return RedirectToAction("index");
        }

        public async Task<RedirectToActionResult> RemoveFromShopCart(int sneakersId)
        {
            var selectedDrink = await context.Sneakers.FirstOrDefaultAsync(s => s.Id == sneakersId);
            if (selectedDrink != null)
                await shoppingCart.RemoveFromCart(selectedDrink);
            return RedirectToAction("index");
        }


    }
}
