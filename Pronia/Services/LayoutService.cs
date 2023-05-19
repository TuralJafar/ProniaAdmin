using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pronia.DAL;
using Pronia.mModels;
using Pronia.ViewModels;

namespace Pronia.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _http;

        public LayoutService(AppDbContext context,IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }
        public async Task<Dictionary<string,string>> GetSettings()
        {
           var settings=await _context.Settings.ToDictionaryAsync(s=>s.Key, s => s.Value);
            return settings;
        }
        public async Task<List<BasketItemVM>> GetBasket()
        {
            List<BasketCookiesItemVM> basket;
            string json = _http.HttpContext.Request.Cookies["Basket"];
            if (!string.IsNullOrEmpty(json)) { basket = JsonConvert.DeserializeObject<List<BasketCookiesItemVM>>(json); }
            else { basket = new List<BasketCookiesItemVM>(); }
            List<BasketItemVM> basketItems = new List<BasketItemVM>();
            foreach (var item in basket)
            {
                Product product = await _context.Products.Include(p => p.ProductImages.Where(pi => pi.IsPrimary == true)).FirstOrDefaultAsync(p => p.Id == item.Id);
                if (product != null)
                {
                    basket.Remove(item);
                    continue;
                }
                BasketItemVM basketItem = new BasketItemVM()
                {
                    Id = product.Id,
                    Image = product.ProductImages.FirstOrDefault().ImageUrl,
                    Name = product.Name,


                };



            }
            return basketItems;


            
        }
    }
}
