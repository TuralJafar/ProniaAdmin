using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.mModels;
using Pronia.ViewModels.Employee;

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
           await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id < 1) return BadRequest();

            Employee existed = await _context.Employees.Include(e=>e.Position).FirstOrDefaultAsync(e => e.Id == id);

            if (existed == null) return NotFound();
            ViewBag.Positions = await _context.Positions.ToListAsync();
            return View(existed);
            return View();
            //Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            //if (employee != null)
            //{
            //    return RedirectToAction("Index");

            //}
            //UpdateEmployeeVM updateEmployeeVM = new UpdateEmployeeVM()
            //{
            //    Name = employee.Name,
            //    PositionId = employee.PositionId,
            //};
            //ViewBag.Positions = _context.Positions.ToList();
            //return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Employee employee)
        {
            if (id == null || id < 1) return BadRequest();

            Employee existed = await _context.Employees.Include(e => e.Position).FirstOrDefaultAsync(e => e.Id == id);

            if (existed == null) return NotFound();
            if (!ModelState.IsValid)
            {
                
                                ViewBag.Positions = await _context.Positions.ToListAsync();

                return View();
            }
            if (existed.PositionId != employee.PositionId)
            {
                bool result = await _context.Positions.AnyAsync(p => p.Id == employee.PositionId);
                if (!result)
                {
                    ModelState.AddModelError("Position", "bu id-li position yokdur");
                    ViewBag.Positions = await _context.Positions.ToListAsync();
                    return View(existed);
                }
                existed.PositionId= employee.PositionId;
            }
            //ViewBag.Positions = await _context.Positions.ToListAsync();
            existed.Name = employee.Name;
                existed.Surname= employee.Surname;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


            public IActionResult Delete()
            {
          return View();
          }
    }
}
