using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.ViewModels;

namespace Pronia.Properties
{   
    public class HomeController:Controller
    {
        private AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index() 
        {
            HomeVM homeVM = new HomeVM
            {
                Products = await _context.Products.Include(p=>p.ProductImages).ToListAsync(),
            };
            return View(homeVM); 
        }
    }
}
