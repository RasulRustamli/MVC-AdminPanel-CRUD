using FrontToBack.DAL;
using FrontToBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly Context _context;
        public CategoryController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Categories
                .Include(p => p.Products)
                .ToList();

            return View(categories);
        }
    }
}
