﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pronia.DAL;
using Pronia.mModels;
using Pronia.ViewModels;

namespace Pronia.Controllers
{
    public class ShopController:Controller
    {
        private readonly AppDbContext _context;
        public ShopController (AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
           
            return View();
        }
        public async Task<IActionResult> Details(int id)
        { //  string result=Request.Cookies["Name"].ToString();
          //    if(string.IsNullOrEmpty(result) )
          //    {
          //        return NotFound();
          //    }
            Response.Cookies.Append("Name","Tural", new CookieOptions
            {
                MaxAge = TimeSpan.FromSeconds(50)
            });


            HttpContext.Session.SetString("Name","Yusif");
            Product product=await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Include (p => p.ProductTags).ThenInclude(pt=>pt.Tag)
                .FirstOrDefaultAsync(p=>p.Id==id);
            if(product==null)
            {
                return NotFound();
            }

            List<Product>products=await _context.Products.Where(p=>p.CategoryId==product.CategoryId&&p.Id!=product.Id).ToListAsync();
            DetailVM detailVM = new DetailVM()
            {
                Product = product,
                Products = products,
            };
            return View();
        }
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) { return NotFound(); }
            List<BasketCookiesItemVM> basket;
            if (Request.Cookies["Basket"] == null)
            {
                 basket = new List<BasketCookiesItemVM>();
                basket.Add(new BasketCookiesItemVM
                {
                    Id = product.Id,
                    Count = 1
                });
            }
            else
            {
                basket = JsonConvert.DeserializeObject<List<BasketCookiesItemVM>>(Request.Cookies["Basket"]);
              BasketCookiesItemVM existed=  basket.FirstOrDefault(b => b.Id == id);
                if(existed!=null)
                {
                    existed.Count++;
                }
                basket.Add(new BasketCookiesItemVM()
                {
                    Id = product.Id,
                    Count = 1
                });
            }
            
            string json = JsonConvert.SerializeObject(basket);
            Response.Cookies.Append("Basket", json);
            return RedirectToAction(nameof(Index), "Home");

        }
        public async Task<IActionResult> GetBasket(int? id)
        {
          var basket=  JsonConvert.DeserializeObject <List<BasketCookiesItemVM>>(Request.Cookies["Basket"]);
           return Json(basket);
        }
    }
}
