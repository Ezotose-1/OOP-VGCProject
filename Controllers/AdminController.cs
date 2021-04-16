using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OOP_VGCProject.Data;
using OOP_VGCProject.DTO;
using OOP_VGCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.Controllers
{
    // To remember : All useless password = Passw0rd_
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdminController(RoleManager<IdentityRole> roleManager,
                               UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

            // HttpGet to get the CreateRole view
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole()
        {
            return View();
        }

            // HttpPost to add a new role from it model
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Admin");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

            // HttpGet to get the role's list
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

            // HttpGet to get the role's list
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ListUsers()
        {
            var usersList = new List<IdentityUser>();
            foreach (var u in userManager.Users)
            {
                usersList.Add(u);
            }

            var users = new List<UserListDTO>();
            foreach (var user in usersList)
            {
                users.Add(new UserListDTO
                            {
                                Id = user.Id,
                                Email = user.Email,
                                Role = userManager.GetRolesAsync(user).Result.FirstOrDefault()
                            }
                         );
            }

            return View(users);
        }


        // HttpGet from it id to get the informations
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            // Retrieve all the Users
            foreach (var user in userManager.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

            // HttpPost to update the role's information
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

            // Set User Admin
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetUserAdmin(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Admin");
            await userManager.RemoveFromRoleAsync(user, "Student");
            await userManager.RemoveFromRoleAsync(user, "Faculty");
            var result = await userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("ListUsers", new { Id = id });
        }

            // Set User Faculty
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetUserFaculty(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Admin");
            await userManager.RemoveFromRoleAsync(user, "Student");
            await userManager.RemoveFromRoleAsync(user, "Faculty");
            var result = await userManager.AddToRoleAsync(user, "Faculty");
            var roles = userManager.GetRolesAsync(user);
            return RedirectToAction("ListUsers", new { Id = id });
        }

            // Set User Faculty
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRole(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Admin");
            await userManager.RemoveFromRoleAsync(user, "Student");
            await userManager.RemoveFromRoleAsync(user, "Faculty");
            return RedirectToAction("ListUsers", new { Id = id });
        }
    }
}
