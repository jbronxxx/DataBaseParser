using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatabaseParser.Models;
using System;
using System.Linq;

namespace DatabaseParser.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["PayrollNumberSort"] = String.IsNullOrEmpty(sortOrder) ? "payrollNumberDesc" : "";
            ViewData["ForeNameSort"]      = String.IsNullOrEmpty(sortOrder) ? "foreNameDesc" : "";
            ViewData["SurNameSort"]       = String.IsNullOrEmpty(sortOrder) ? "surNameDesc" : "";
            ViewData["DateOfBirthSort"]   = String.IsNullOrEmpty(sortOrder) ? "dateOfBirthDesc" : "";
            ViewData["TelephoneSort"]     = String.IsNullOrEmpty(sortOrder) ? "telephoneDesc" : "";
            ViewData["MobileSort"]        = String.IsNullOrEmpty(sortOrder) ? "mobileDesc" : "";
            ViewData["AdressSort"]        = String.IsNullOrEmpty(sortOrder) ? "adressDesc" : "";
            ViewData["Adress2Sort"]       = String.IsNullOrEmpty(sortOrder) ? "adress2Desc" : "";
            ViewData["PostCodeSort"]      = String.IsNullOrEmpty(sortOrder) ? "postCodeDesc" : "";
            ViewData["EmailHomeSort"]     = String.IsNullOrEmpty(sortOrder) ? "emailHomeDesc" : "";
            ViewData["StartDateSort"]     = String.IsNullOrEmpty(sortOrder) ? "startDateDesc" : "";

            ViewData["SearchFilter"]      = searchString;

            var employees = from s in _context.Employees select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.SurName.Contains(searchString) || s.ForeNames.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "payrollNumberDesc":
                    employees = employees.OrderByDescending(e => e.PayrollNumber);
                    break;
                case "foreNameDesc":
                    employees = employees.OrderByDescending(e => e.ForeNames);
                    break;
                case "surNameDesc":
                    employees = employees.OrderByDescending(e => e.SurName);
                    break;
                case "dateOfBirthDesc":
                    employees = employees.OrderByDescending(e => e.DateOfBirth);
                    break;
                case "telephoneDesc":
                    employees = employees.OrderByDescending(e => e.Telephone);
                    break;
                case "mobileDesc":
                    employees = employees.OrderByDescending(e => e.Mobile);
                    break;
                case "adressDesc":
                    employees = employees.OrderByDescending(e => e.Adress);
                    break;
                case "adress2Desc":
                    employees = employees.OrderByDescending(e => e.Adress2);
                    break;
                case "postCodeDesc":
                    employees = employees.OrderByDescending(e => e.PostCode);
                    break;
                case "emailHomeDesc":
                    employees = employees.OrderByDescending(e => e.EmailHome);
                    break;
                case "startDateDesc":
                    employees = employees.OrderByDescending(e => e.StartDate);
                    break;
                default:
                    //employees = employees.OrderBy(e => e.SurName);
                    break;
            }

            return View(await employees.AsNoTracking().ToListAsync());
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
    }
}