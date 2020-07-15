namespace SS.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Sneaker Sneaker { get; set; }
        public int Quantity { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
