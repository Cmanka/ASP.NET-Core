using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using SS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SS.Controllers
{
    [Authorize(Roles = "admin")]
    public class BrandController : Controller
    {
        MyContext context;
        public BrandController(MyContext context)
        {
            this.context = context;
        }
        public IActionResult Index() => View(context.Brands.ToList());
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(Brand brand)
        {
            if (ModelState.IsValid)
            {
                var newBrand = new Brand
                {
                    Name = brand.Name
                };
                await context.Brands.AddAsync(newBrand);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(brand);
        }
        [HttpGet]
        [ActionName("Remove")]
        public async Task<IActionResult> ConfirmRemove(int? id)
        {
            if (id != null)
            {
                Brand brand = await context.Brands.FirstOrDefaultAsync(b => b.Id == id);
                if (brand != null)
                    return View(brand);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Remove(int? id)
        {
            if (id != null)
            {
                Brand brand = await context.Brands.FirstOrDefaultAsync(b => b.Id == id);

                if (brand != null)
                {
                    try
                    {
                        context.Brands.Remove(brand);
                        await context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ExceptionMessage = ex.InnerException.Message;
                        return View("Error");
                    }
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Brand brand = await context.Brands.FirstOrDefaultAsync(b => b.Id == id);
                if (brand != null)
                    return View(brand);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                context.Brands.Update(brand);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(brand);
        }
    }
}
