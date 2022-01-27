using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly Context _context;
        public HomeController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HttpContext.Session.SetString("person", "lorem");
            
            //Response.Cookies.Append("basket", "product", new CookieOptions() { MaxAge = TimeSpan.FromMinutes(10)});

            List<Slider> sliders = _context.Sliders.ToList();
            SliderDesc sliderDesc = _context.SliderDescs.FirstOrDefault();
            List<Category> categories = _context.Categories.ToList();
            About about = _context.Abouts.FirstOrDefault();
            List<Expert> experts = _context.Experts.ToList();
            List<Blogs> blogs = _context.Blogs.ToList();
            List<BlogsSlider> blogslider = _context.BlogsSliders.ToList();
            List<InstagramSlider> instagram = _context.InstagramSliders.ToList();


            HomeVm homeVm = new HomeVm();
            homeVm.Sliders = sliders;
            homeVm.SliderDesc = sliderDesc;
            homeVm.Categories = categories;
            homeVm.About = about;
            homeVm.Experts = experts;
            homeVm.Blogs = blogs;
            homeVm.BlogsSliders = blogslider;
            homeVm.InstagramSliders = instagram;

            return View(homeVm);
        }
        public IActionResult GetSession()
        {
           string session= HttpContext.Session.GetString("person");
          // string cookie= Request.Cookies["basket"];
            return Content(session);
        }

    
    }
}
