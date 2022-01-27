using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Controllers
{

    public class BasketController : Controller
    {
        private readonly Context _context;
        public BasketController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Error");
            Product product =await _context.Products.FindAsync(id);
            if(product==null) return RedirectToAction("Index", "Error");
            string basket = Request.Cookies["basket"];
            List<BasketProduct> basketProductList;
            if (basket==null)
            {
                basketProductList = new List<BasketProduct>();
            }
            else
            {
                basketProductList = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);
            }

            BasketProduct isExistProduct = basketProductList.FirstOrDefault(p => p.Id == product.Id);
            if (isExistProduct == null)
            {
                BasketProduct basketProduct = new BasketProduct
                {
                    Id=product.Id,
                    Count = 1
                };
                basketProductList.Add(basketProduct);
            }
            else
            {
                isExistProduct.Count++;
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProductList), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });

            return RedirectToAction("Index","Home");
        }
        public IActionResult ShowBasket()
        {
            string basket=Request.Cookies["basket"];
            List<BasketProduct> products = new List<BasketProduct>();
            if (basket != null)
            {
                products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);
                foreach (var item in products)
                {

                  Product product=  _context.Products.FirstOrDefault(p => p.Id == item.Id);
                    item.Price = product.Price;
                    item.ImageUrl = product.ImageUrl;
                    item.Name = product.Name;

                }
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });

            }
            return View(products);
        }
    }
}
