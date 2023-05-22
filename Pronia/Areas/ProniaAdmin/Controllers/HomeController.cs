using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.ProniaAdmin.Controllers
{
    public class HomeController : Controller
    {
        [Area("ProniaAdmin")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
