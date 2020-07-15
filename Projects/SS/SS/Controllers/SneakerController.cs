using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using SS.ViewModels;
using SS.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SS.Controllers
{
    [Authorize(Roles="admin")]
    public class SneakerController : Controller
    {
        MyContext context;
        IWebHostEnvironment webHost;
        SneakerAddViewModel savm;
        public SneakerController(MyContext context, IWebHostEnvironment webHost)
        {
            this.context = context;
            this.webHost = webHost;
            savm = new SneakerAddViewModel() { BrandsList = this.context.Brands.ToList(), Sneakers = this.context.Sneakers.ToList() };
        }
        public IActionResult Index() => View(savm);
        public IActionResult Add() => View(savm);

        [HttpPost]
        public async Task<IActionResult> Add(SneakerAddViewModel model)
        {
            model.BrandsList = savm.BrandsList;
            if (ModelState.IsValid)
            {
                string FileName = null;
                string Folder = Path.Combine(webHost.WebRootPath, "Images");
                FileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                string FilePath = Path.Combine(Folder, FileName);
                await model.Picture.CopyToAsync(new FileStream(FilePath, FileMode.Create));
                model.PicturePath = FileName;
                var sneaker = new Sneaker()
                {
                    Name = model.Name,
                    BrandId = model.BrandId,
                    Price = model.Price,
                    Count = model.Count,
                    Color = model.Color,
                    PicturePath = FileName
                };
                await context.Sneakers.AddAsync(sneaker);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        [ActionName("Remove")]
        public async Task<IActionResult> ConfirmRemove(int? id)
        {
            if (id != null)
            {
                Sneaker sneakers = await context.Sneakers.FirstOrDefaultAsync(s => s.Id == id);
                if (sneakers != null)
                    return View(sneakers);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Remove(int? id)
        {
            if (id != null)
            {
                Sneaker sneakers = await context.Sneakers.FirstOrDefaultAsync(s => s.Id == id);
                if (sneakers != null)
                {
                    context.Sneakers.Remove(sneakers);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Sneaker sneakers = await context.Sneakers.FirstOrDefaultAsync(s => s.Id == id);
                SneakerAddViewModel model = new SneakerAddViewModel()
                {
                    BrandsList = savm.BrandsList,
                    SneakerId = sneakers.Id,
                    Name = sneakers.Name,
                    BrandId = sneakers.BrandId,
                    Price = sneakers.Price,
                    Color = sneakers.Color,
                    Count = sneakers.Count,
                    PicturePath = sneakers.PicturePath
                };

                if (model != null)
                    return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SneakerAddViewModel model)
        {
            model.BrandsList = savm.BrandsList;
            var sneaker = new Sneaker()
            {
                Id = model.SneakerId,
                PicturePath = model.PicturePath,
                Name = model.Name,
                BrandId = model.BrandId,
                Price = model.Price,
                Count = model.Count,
                Color = model.Color
            };
            context.Sneakers.Update(sneaker);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
