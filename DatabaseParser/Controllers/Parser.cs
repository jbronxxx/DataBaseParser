using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DatabaseParser.Models
{
    public class Parser : Controller
    {
        private IWebHostEnvironment _env;

        public Parser(IWebHostEnvironment env)
        {
            _env = env;
        }

        // POST: Upload database from file
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
            return RedirectToAction("Index","Employee");
        }
    }
}