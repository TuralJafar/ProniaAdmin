﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.ViewModels;

namespace Pronia.Properties
{   
    public class HomeController:Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index() 
        {
            //Response.Cookies.Append("Name", "Tural", new CookieOptions
            //{
            //    MaxAge = TimeSpan.FromSeconds(10)
            //});
            //HttpContext.Session.SetString("Name","Yusif");
            HomeVM homeVM = new HomeVM
            {
                Products = await _context.Products.Include(p=>p.ProductImages).ToListAsync(),
            };
            return View(homeVM); 
        }
    }
}
