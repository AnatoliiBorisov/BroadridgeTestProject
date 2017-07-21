using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BroadridgeTestProject.Infrastructure.Identity;
using BroadridgeTestProject.Models;
using BroadridgeTestProject.Models.Identity;
using BroadridgeTestProject.Models.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BroadridgeTestProject.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var adminIndexModel = new AdminIndexModel
            {
                Roles = RoleManager.Roles,
                Users = UserManager.Users
            };

            return View(adminIndexModel);
        }

        #region Users

        public ActionResult UserCreate()
        {
            return View("User/Create");
        }

        [HttpPost]
        public async Task<ActionResult> UserCreate(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View("User/Create", model);
        }

        [HttpPost]
        public async Task<ActionResult> UserDelete(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "User Not Found" });
            }
        }

        public async Task<ActionResult> UserEdit(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View("User/Edit", user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UserEdit(string id, string email, string password, City? city = null)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                user.City = city;

                var validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    validPass = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validEmail.Succeeded && validPass == null) 
                    || (validEmail.Succeeded && password != string.Empty && validPass?.Succeeded == true))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }
        #endregion Users

        #region Roles

        public ActionResult RoleCreate()
        {
            return View("Role/Create");
        }

        [HttpPost]
        public async Task<ActionResult> RoleCreate([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new AppRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View("Role/Create", name);
        }

        [HttpPost]
        public async Task<ActionResult> RoleDelete(string id)
        {
            AppRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Role Not Found" });
            }
        }

        public async Task<ActionResult> RoleEdit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            var memberIDs = role.Users.Select(x => x.UserId).ToArray();

            var members = UserManager.Users.Where(x => memberIDs.Any(y => y == x.Id));
            var nonMembers = UserManager.Users.Except(members);

            var roleEditModel = new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };

            return View("Role/Edit", roleEditModel);
        }

        [HttpPost]
        public async Task<ActionResult> RoleEdit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    result = await UserManager.AddToRoleAsync(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    result = await UserManager.RemoveFromRoleAsync(userId,
                    model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                return RedirectToAction("Index");
            }
            return View("Error", new string[] { "Role Not Found" });
        }
        #endregion Roles

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}