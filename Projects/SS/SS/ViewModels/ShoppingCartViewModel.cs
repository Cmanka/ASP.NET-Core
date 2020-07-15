using System.Collections.Generic;
using SS.Models;

namespace SS.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
    }
}
