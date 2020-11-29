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

        public Parser(IWebHostEnvironment env)
        {
            _env = env;
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

            return RedirectToAction("Index","Employee");
        }

        private List<string> GetFileLinesToList(IFormFile fName)
        {
            List<string> fileLines = new List<string>();

            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using(var streamReader = new StreamReader(fileName))
                {
                    while (!streamReader.EndOfStream)
                    {
                        fileLines.Add(streamReader.ReadLine());
                    }
                }
            }

            return fileLines;
        }
    }
}