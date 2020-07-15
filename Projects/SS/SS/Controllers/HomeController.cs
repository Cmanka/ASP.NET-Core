using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SS.Models;
using SS.ViewModels;

namespace SS.Controllers
{
    public class HomeController : Controller
    {
        MyContext context;
        List<Sneaker> sneakers;
        List<Brand> brands;
        public HomeController(MyContext context)
        {
            this.context = context;
            sneakers = this.context.Sneakers.ToList();
            brands = this.context.Brands.ToList();
        }
        public IActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel() { Sneakers = sneakers, Brands = brands };
            return View(ivm);
        }
    }
}
