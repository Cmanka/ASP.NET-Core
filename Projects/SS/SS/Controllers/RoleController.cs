using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace SS.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult Index() => View(roleManager.Roles.ToList());
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }
        [HttpGet]
        [ActionName("Remove")]
        public async Task<IActionResult> ConfirmRemove(string id)
        {
            if (id != null)
            {
                IdentityRole role = await roleManager.FindByIdAsync(id);
                if (role != null)
                    return View(role);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Remove(string id)
        {
            if (id != null)
            {
                IdentityRole role = await roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    await roleManager.DeleteAsync(role);
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
