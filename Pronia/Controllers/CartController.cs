using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pronia.DAL;
using Pronia.mModels;
using Pronia.ViewModels;
using System.Collections.Generic;
using System.Net;

namespace Pronia.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<BasketCookiesItemVM> basket;
            string json = Request.Cookies["Basket"];
            if (!string.IsNullOrEmpty(json)) { basket = JsonConvert.DeserializeObject<List<BasketCookiesItemVM>>(json); }
            else {basket=new List<BasketCookiesItemVM>(); }
            List<BasketItemVM> basketItems = new List<BasketItemVM>();
            foreach (var item in basket) 
            {
                Product product = await _context.Products.Include(p => p.ProductImages.Where(pi => pi.IsPrimary == true)).FirstOrDefaultAsync(p => p.Id == item.Id);
                if(product != null)
                {
                    basket.Remove(item);
                    continue;
                }
                BasketItemVM basketItem = new BasketItemVM()
                {
                    Id=product.Id,
                    Image=product.ProductImages.FirstOrDefault().ImageUrl,
                    Name=product.Name,
                    
                    
                };
                
                        

            }
            
           
            return View(basketItems);
        }
    }
}
