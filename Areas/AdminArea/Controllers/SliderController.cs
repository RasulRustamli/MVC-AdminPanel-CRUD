using FrontToBack.DAL;
using FrontToBack.Extensions;
using FrontToBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Areas.AdminArea.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(Context context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult>Create(Slider slider)
        //{
        //    if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
        //    {
        //        ModelState.AddModelError("Photo", "Do not empty");
        //    }

        //    if (!slider.Photo.IsImage())
        //    {
        //        ModelState.AddModelError("Photo", "only image");
        //        return View();
        //    }
        //    if (slider.Photo.IsCorrectSize(300))
        //    {
        //        ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
        //        return View();
        //    }

        //    Slider newSlider = new Slider();

        //    string fileName = await slider.Photo.SaveImageAsync(_env.WebRootPath, "img");
        //    newSlider.ImageUrl = fileName;

        //    await  _context.Sliders.AddAsync(newSlider);
        //    await _context.SaveChangesAsync();


        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Slider slider)
        {
            if (ModelState["Photos"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photos", "Do not empty");
            }
            foreach (IFormFile photo in slider.Photos)
            {
                if (!photo.IsImage())
                {
                    ModelState.AddModelError("Photos", "only image");
                    return View();
                }
                if (photo.IsCorrectSize(300))
                {
                    ModelState.AddModelError("Photos", "300den yuxari ola bilmez");
                    return View();
                }

                Slider newSlider = new Slider();

                string fileName = await photo.SaveImageAsync(_env.WebRootPath, "img");
                newSlider.ImageUrl = fileName;

                await _context.Sliders.AddAsync(newSlider);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int?id)
        {
            if (id == null) return NotFound();
            Slider dbSlider = await _context.Sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();
            return View(dbSlider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSlider(int? id)
        {
            if (id == null) return NotFound();
            Slider dbSlider = await _context.Sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath,"img", dbSlider.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Sliders.Remove(dbSlider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Slider dbSlider = await _context.Sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();
            return View(dbSlider);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Update(int? id, Slider slider)
        //{
        //    if (id == null) return NotFound();

        //    if (slider.Photo != null)
        //    {
        //        if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
        //        {
        //            ModelState.AddModelError("Photo", "Do not empty");
        //        }

        //        if (!slider.Photo.IsImage())
        //        {
        //            ModelState.AddModelError("Photo", "only image");
        //            return View();
        //        }
        //        if (slider.Photo.IsCorrectSize(300))
        //        {
        //            ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
        //            return View();
        //        }
        //        Slider dbSlider =await  _context.Sliders.FindAsync(id);
        //        string path = Path.Combine(_env.WebRootPath, "img", dbSlider.ImageUrl);
        //        if (System.IO.File.Exists(path))
        //        {
        //            System.IO.File.Delete(path);
        //        }
        //        string fileName = await slider.Photo.SaveImageAsync(_env.WebRootPath, "img");
        //        dbSlider.ImageUrl = fileName;
        //        await _context.SaveChangesAsync();
        //    }
        //    return RedirectToAction("Index");
        //}

    }
}
