using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.mModels;

namespace ProniaBB102Web.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SlideController : Controller
    {   private readonly AppDbContext _context;
      

        public SlideController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {List<Slide>slides=await _context.Slides.ToListAsync();
            return View(slides);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slide slide)
        {
            if (slide.Photo != null)
            {
                ModelState.AddModelError("Photo", "Sekil hissesi bos ola bilmez");
                return View();
            }
            if(!slide.Photo.ContentType.Contains("image/")) 
            {
                ModelState.AddModelError("Photo", "Sekil daxil edin");
                return View();
            }
            if (slide.Photo.Length > 2000 * 1024)
            {
                ModelState.AddModelError("Photo", "sekil 2mbdan coxdu");
            }
            if (slide.Id == slide.Id)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            bool result = _context.Categories.Any(c => c.Name.Trim().ToLower() == slide.Description.Trim().ToLower() && c.Id !=slide.Id);

            if (result)
            {
                ModelState.AddModelError("Id", "Bu id-de categoriya artiq movcuddur");
                return View();
            }
            FileStream file = new FileStream(@"C:\Users\tural\source\repos\Pronia\Pronia\wwwroot\assets\images\website-images\" + slide.Photo.FileName, FileMode.Create);
            await slide.Photo.CopyToAsync(file);
            slide.Image = slide.Photo.FileName;

            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}