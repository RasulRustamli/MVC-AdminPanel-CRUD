
using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("AdminArea")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Context _context;

        public UserController(
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
            var users = _userManager.Users;

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();


            AppUser user = new AppUser()
            {
                FullName = register.FullName,
                UserName = register.UserName,
                Email = register.Email,
            };

            IdentityResult identityResult = await _userManager.CreateAsync(user, register.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(user, true);

            return RedirectToAction("Index", "User", new { area = "AdminArea" });
        }

        public async Task<IActionResult> IsActive(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);

          

            if (user == null)
            {
                return NotFound();
            }
            if (user.IsActive)
            {
                user.IsActive = false;
            }
            else
            {
                user.IsActive = true;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}