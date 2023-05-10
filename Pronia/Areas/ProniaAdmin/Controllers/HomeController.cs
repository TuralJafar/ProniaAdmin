using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.ProniaAdmin.Controllers
{
    public class HomeController : Controller
    {
        [Area("ProniaAdmin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
