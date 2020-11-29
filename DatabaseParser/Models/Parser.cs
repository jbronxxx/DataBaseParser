using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatabaseParser.Models
{
    public class StartData
    {
        internal static List<string> dbFromFile { get; set; }

        public List<string> ReadFile(FileModel fileModel)
        {
            dbFromFile = File.ReadAllLines(fileModel.FilePath).ToList();

            return dbFromFile;
        }

        [HttpPost]
        public static void Initialize(EmployeeContext context)
        {
            if (!context.Employees.Any() && dbFromFile != null)
            {
                for (int i = 1; i <= dbFromFile.Count; i++)
                {
                    context.Employees.Add(new Employee());
                }

                context.SaveChanges();
            }
        }
    }
}