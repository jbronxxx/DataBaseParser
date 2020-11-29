using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using DatabaseParser.Models;

namespace DatabaseParser.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;

        private IWebHostEnvironment _env;

        public EmployeeController(EmployeeContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employee/Add Or Edit
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Employee());
            else
                return View(_context.Employees.Find(id));

        }

        // POST: Employee/Add or edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("PayrollNumber,ForeNames,SurName,DateOfBirth,Telephone,Mobile,Adress,Adress2,PostCode,EmailHome,StartDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.EmployeeId == 0)
                {
                    _context.Add(employee);
                }
                else
                    _context.Update(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Upload database from file
        [HttpPost]
        public IActionResult DataBaseUpload(IFormFile file)
        {
            var dir = _env.WebRootPath;

            if (file != null)
            {
                using (var fileStream = new FileStream(Path.Combine(dir, file.FileName), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
            }
            return RedirectToAction("Index");
        }
    }
}