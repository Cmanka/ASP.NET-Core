using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS.Models;

namespace SS.Controllers
{
    public class OrderController : Controller
    {
        MyContext context;
        ShoppingCart shoppingCart;
        public OrderController(MyContext context, ShoppingCart shoppingCart)
        {
            this.context = context;
            this.shoppingCart = shoppingCart;
        }
        public IActionResult Checkout() => View();
        public IActionResult CheckoutComplete() => View();
        [HttpPost]
        [Authorize]
        public IActionResult CheckOut(Order order)
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            if (shoppingCart.ShoppingCartItems.Count == 0)
                ModelState.AddModelError("", "Your card is empty");
            if (ModelState.IsValid)
            {
                MakeOrder(order);
                shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }
        private void MakeOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            context.Orders.Add(order);
            context.SaveChanges();

            var shoppingCartItems = shoppingCart.ShoppingCartItems;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Quantity = shoppingCartItem.Quantity,
                    SneakerId = shoppingCartItem.Sneaker.Id,
                    OrderId = order.Id,
                    Price = shoppingCartItem.Sneaker.Price
                };
                context.OrderDetails.Add(orderDetail);
                orderDetail.Sneaker.Count--;
                context.Sneakers.Update(orderDetail.Sneaker);
            }

            context.SaveChanges();
        }
    }
}
