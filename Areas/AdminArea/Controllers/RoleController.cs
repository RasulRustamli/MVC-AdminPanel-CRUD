using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Context _context;

        public RoleController(
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            Context context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(string role)
        {
            if (!string.IsNullOrEmpty(role))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                    // await _roleManager.DeleteAsync(new IdentityRole(role));
                }
                return RedirectToAction("index");
            }

            return NotFound();
        }

        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            UpdateRoleVM updateUserRole = new UpdateRoleVM
            {
                User = user,
                Userid = user.Id,
                Roles = _roleManager.Roles.ToList(),
                UserRoles = await _userManager.GetRolesAsync(user)

            };
            return View(updateUserRole);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(string id, List<string> roles)
        {

            if(roles.Count > 0)
            {
                var user = await _userManager.FindByIdAsync(id);
                var dbRoles = _roleManager.Roles.ToList();
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRole = roles.Except(userRoles);
                var removedRole = userRoles.Except(roles);
                await _userManager.AddToRolesAsync(user, addedRole);
                await _userManager.RemoveFromRolesAsync(user, removedRole);
            }
            else
            {
             return RedirectToAction("update");
            }
             return RedirectToAction("update");
        }


        public async Task<IActionResult> Delete(string id)
        {
            
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role.ToString().ToLower() == "admin" || role.ToString().ToLower() == "superadmin")
            {
                return RedirectToAction("role");
            }
            if(role != null)
            {
              await  _roleManager.DeleteAsync(role);
            }

            return RedirectToAction("role");
        }
    }
}