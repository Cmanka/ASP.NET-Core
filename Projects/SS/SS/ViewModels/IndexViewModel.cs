using System.Collections.Generic;
using SS.Models;

namespace SS.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Sneaker> Sneakers { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
    }
}
