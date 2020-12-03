using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseParser.Models
{
    public class Parser : Controller
    {
        private IWebHostEnvironment _env;

        private EmployeeContext _context;

        public Parser(IWebHostEnvironment env, EmployeeContext context)
        {
            _env = env;
            _context = context;
        }

        // POST: Upload .csv file to wwwroot/files
        [HttpPost]
        public IActionResult DataBaseUpload(IFormFile file)
        {
            var dir = _env.WebRootPath;

            if (file != null)
            {
                using (var fileStream = new FileStream(Path.Combine(dir, "files", file.FileName), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
            }

            // Adding an employee to the database 
            if (file != null)
            {
                var employeesFromFile = GetEmployeeList(file.FileName);
                
                if (ModelState.IsValid)
                {
                    {
                        foreach (var emp in employeesFromFile)
                        {
                            if (!_context.Employees.Any(o => o.PayrollNumber == emp.PayrollNumber))
                            {
                                _context.Add(emp);
                            }
                        }
                        TempData["notice"] = $"Successfully uploaded {employeesFromFile.Count} employees.";
                    }
                }
                else
                    _context.Update(employeesFromFile);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index","Employee");
        }

        // Reading .csv file
        private List<Employee> GetEmployeeList(string fName)
        {
            List<string> fileLines = new List<string>();

            List<Employee> employees = new List<Employee>();

            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using(var streamReader = new StreamReader(fileName))
                {
                    while (!streamReader.EndOfStream)
                    {
                        fileLines.Add(streamReader.ReadLine());
                    }
                }
            }

            for (int i = 1; i < fileLines.Count; i++)
            {
                string[] temp = fileLines[i].Split(',');

                employees.Add(new Employee()
                {
                    PayrollNumber = temp[0].ToString(),
                    ForeNames     = temp[1].ToString(),
                    SurName       = temp[2].ToString(),
                    DateOfBirth   = temp[3].ToString(),
                    Telephone     = temp[4].ToString(),
                    Mobile        = temp[5].ToString(),
                    Adress        = temp[6].ToString(),
                    Adress2       = temp[7].ToString(),
                    PostCode      = temp[8].ToString(),
                    EmailHome     = temp[9].ToString(),
                    StartDate     = temp[10].ToString()
                });
            }

            return employees;
        }
    }
}