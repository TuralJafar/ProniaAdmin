﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        {
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

    }
}
