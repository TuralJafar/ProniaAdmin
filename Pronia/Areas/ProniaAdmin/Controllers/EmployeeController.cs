using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.mModels;

namespace Pronia.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _context.Employees.Include(e => e.Position).ToListAsync();

            return View(employees);
           
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Positions=await _context.Positions.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {   bool result=await _context.Positions.AnyAsync(p=>p.Id==employee.PositionId);
            if(!result) 
            {
                ModelState.AddModelError("Position", "bu id-li position yokdur");
                ViewBag.Positions = await _context.Positions.ToListAsync();
                return View();
            }
            return Json(employee);
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
