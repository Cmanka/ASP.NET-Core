using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SS.Models;
using SS.ViewModels;

namespace SS.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        UserManager<User> userManager;
        RoleManager<IdentityRole> roleManager;
        UserViewModel uvm;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            uvm = new UserViewModel { RolesList = roleManager.Roles.ToList() };
        }

        public IActionResult Index() => View(userManager.Users.ToList());

        public IActionResult Add() => View(uvm);

        [HttpPost]
        public async Task<IActionResult> Add(UserViewModel model)
        {
            model.RolesList = uvm.RolesList;
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, Role = model.Role, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, model.Role);
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
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, UserName = user.UserName, Role = user.Role, RolesList = uvm.RolesList };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    user.UserName = model.UserName;
                    user.Role = model.Role;
                    var result = await userManager.UpdateAsync(user);
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
            }
            return View(model);
        }
        [HttpGet]
        [ActionName("Remove")]
        public async Task<IActionResult> ConfirmRemove(string id)
        {
            if (id != null)
            {
                User user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult> Remove(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
