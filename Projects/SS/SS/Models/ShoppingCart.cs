using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SS.Models
{
    public class ShoppingCart
    {
        MyContext context;
        public string ShoppingCartId { get; set; }
        public List<CartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(MyContext context)
        {
            this.context = context;
        }
       
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var cont = services.GetService<MyContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(cont) { ShoppingCartId = cartId };
        }
        public void AddToCart(Sneaker sneakers,int quantity)
        {
            var shoppingCartItem = context.ShoppingCartItems.SingleOrDefault(
                s => s.Sneaker.Id == sneakers.Id &&
                s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new CartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Sneaker = sneakers,
                    Quantity = 1
                };
                context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
                shoppingCartItem.Quantity++;

            context.SaveChanges(); 
        }
        public async Task<int> RemoveFromCart(Sneaker sneakers)
        {
            var shoppingCartItem = await context.ShoppingCartItems.SingleOrDefaultAsync(
              s => s.Sneaker.Id == sneakers.Id &&
              s.ShoppingCartId == ShoppingCartId);

            var localQuantity = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                    localQuantity = shoppingCartItem.Quantity;
                }
                else
                    context.ShoppingCartItems.Remove(shoppingCartItem);
            }

            await context.SaveChangesAsync();

            return localQuantity;
        }
        public List<CartItem> GetShoppingCartItems() => ShoppingCartItems ?? (
            ShoppingCartItems = context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
            .Include(s => s.Sneaker)
            .ToList());
        public void ClearCart()
        {
            var cartItems = context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId);
            context.ShoppingCartItems.RemoveRange(cartItems);
            context.SaveChanges();
        }
        public decimal GetTotal() => context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
            .Select(c => c.Sneaker.Price * c.Quantity).Sum();
    }
}
