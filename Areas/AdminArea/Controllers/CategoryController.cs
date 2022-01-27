using FrontToBack.DAL;
using FrontToBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Areas.AdminArea.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly Context _context;
        public CategoryController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();

            return View(categories);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Category dbCategory =await  _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();
            return View(dbCategory);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View();
            bool isExist = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adla category var");
                return View();
            }
            Category newCategory = new Category
            {
                Name = category.Name,
                Description = category.Description
            };

             await _context.AddAsync(newCategory);
             await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int?id)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();
            return View(dbCategory);
        }
        [HttpPost]
        public async  Task<IActionResult> Update(Category category)
        {
            if (!ModelState.IsValid) return View();
            bool isExist = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower().Trim());

            Category isExistCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
                                      
            if (isExist&&!(isExistCategory.Name.ToLower()==category.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Bu adla category var");
                return View();
            };

            isExistCategory.Name = category.Name;
            isExistCategory.Description = category.Description;
           await _context.SaveChangesAsync();
            

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int?id)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();
            return View(dbCategory);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(int?id)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();
            _context.Categories.Remove(dbCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
