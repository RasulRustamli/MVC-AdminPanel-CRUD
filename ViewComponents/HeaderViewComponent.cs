using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    ViewBag.Username = user.FullName;
                }
            }
            ViewBag.ProductCount = 0;
            if (Request.Cookies["basket"] != null)
            {
              //  int total = 0;

                List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies["basket"]);
                //foreach (var item in products)
                //{
                //    total+=item.Count;
                //}

                //ViewBag.ProductCount = total ;
                ViewBag.ProductCount = products.Count;
            }
            Bio bio = _context.Bios.FirstOrDefault();
            return View(await Task.FromResult(bio));

        }
    }
}
