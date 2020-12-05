using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatabaseParser.Models;
using System;
using System.Linq;
using System.Net;

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
        public async Task<IActionResult> Index(string searchString, SortOrder sortOrder = SortOrder.SurNameAsc)
        {
            ViewData["PayrollNumberSort"] = sortOrder == SortOrder.PayrollNumberAsc ? SortOrder.PayrollNumberDesc : SortOrder.PayrollNumberAsc;
            ViewData["ForeNameSort"]      = sortOrder == SortOrder.ForeNameAsc ? SortOrder.ForeNameDesc : SortOrder.ForeNameAsc;
            ViewData["SurNameSort"]       = sortOrder == SortOrder.SurNameAsc ? SortOrder.SurNameDesc : SortOrder.SurNameAsc;
            ViewData["DateOfBirthSort"]   = sortOrder == SortOrder.DateOfBirthAsc ? SortOrder.DateOfBirthDesc : SortOrder.DateOfBirthAsc;
            ViewData["TelephoneSort"]     = sortOrder == SortOrder.TelephoneAsc ? SortOrder.TelephoneDesc : SortOrder.TelephoneAsc;
            ViewData["MobileSort"]        = sortOrder == SortOrder.MobileAsc ? SortOrder.MobileDesc : SortOrder.MobileAsc;
            ViewData["AdressSort"]        = sortOrder == SortOrder.AdressAsc ? SortOrder.AdressDesc : SortOrder.AdressAsc;
            ViewData["Adress2Sort"]       = sortOrder == SortOrder.Adress2Asc ? SortOrder.Adress2Desc : SortOrder.Adress2Asc;
            ViewData["PostCodeSort"]      = sortOrder == SortOrder.PostCodeAsc ? SortOrder.PostCodeDesc : SortOrder.PostCodeAsc;
            ViewData["EmailHomeSort"]     = sortOrder == SortOrder.EmailHomeAsc ? SortOrder.EmailHomeDesc : SortOrder.EmailHomeAsc;
            ViewData["StartDateSort"]     = sortOrder == SortOrder.StartDateAsc ? SortOrder.StartDateDesc : SortOrder.StartDateAsc;

            ViewData["SearchFilter"]      = searchString;

            IQueryable<Employee> employees = _context.Employees;

            // Searching by name
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.SurName.Contains(searchString) || s.ForeNames.Contains(searchString));
            }

            // Sorting table
            employees = sortOrder switch
            {
                SortOrder.PayrollNumberDesc => employees.OrderByDescending(s => s.PayrollNumber),
                SortOrder.PayrollNumberAsc  => employees.OrderBy(s => s.PayrollNumber),

                SortOrder.ForeNameDesc      => employees.OrderByDescending(s => s.ForeNames),
                SortOrder.ForeNameAsc       => employees.OrderBy(s => s.ForeNames),

                SortOrder.SurNameDesc       => employees.OrderByDescending(s => s.SurName),
                SortOrder.SurNameAsc        => employees.OrderBy(s => s.SurName),

                SortOrder.DateOfBirthDesc   => employees.OrderByDescending(s => s.DateOfBirth),
                SortOrder.DateOfBirthAsc    => employees.OrderBy(s => s.DateOfBirth),

                SortOrder.TelephoneDesc     => employees.OrderByDescending(s => s.Telephone),
                SortOrder.TelephoneAsc      => employees.OrderBy(s => s.Telephone),

                SortOrder.MobileDesc        => employees.OrderByDescending(s => s.Mobile),
                SortOrder.MobileAsc         => employees.OrderBy(s => s.Mobile),

                SortOrder.AdressDesc        => employees.OrderByDescending(s => s.Adress),
                SortOrder.AdressAsc         => employees.OrderBy(s => s.Adress),

                SortOrder.Adress2Desc       => employees.OrderByDescending(s => s.Adress2),
                SortOrder.Adress2Asc        => employees.OrderBy(s => s.Adress2),

                SortOrder.PostCodeDesc      => employees.OrderByDescending(s => s.PostCode),
                SortOrder.PostCodeAsc       => employees.OrderBy(s => s.PostCode),

                SortOrder.EmailHomeDesc     => employees.OrderByDescending(s => s.EmailHome),
                SortOrder.EmailHomeAsc      => employees.OrderBy(s => s.EmailHome),

                SortOrder.StartDateDesc     => employees.OrderByDescending(s => s.StartDate),
                SortOrder.StartDateAsc      => employees.OrderBy(s => s.StartDate),

                _                           => employees.OrderBy(s => s.SurName)
            };

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
        public async Task<IActionResult> AddOrEdit([Bind("EmployeeId,PayrollNumber,ForeNames,SurName,DateOfBirth,Telephone,Mobile,Adress,Adress2,PostCode,EmailHome,StartDate")] Employee employee)
         {
            if (ModelState.IsValid)
            {
                if (employee == null)
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

        // GET: Employee/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            var employee = await _context.Employees.FindAsync(id);

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}