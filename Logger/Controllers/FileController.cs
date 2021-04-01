using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Controllers
{
    public class FileController : Controller
    {
        public IActionResult Index()
        {
            var writeRecords = new List<Employee> {
                new Employee { Id = 1, Name = "Ina", Birthday=DateTime.Now },
                new Employee { Id = 2, Name = "Duran", Birthday=DateTime.Now},
            };

            var config = new CsvConfiguration(CultureInfo.InvariantCulture);

            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms, Encoding.UTF8);
            using (var csv = new CsvWriter(writer, config))
                csv.WriteRecords(writeRecords);

            return File(ms.ToArray(), "text/csv", $"export_{DateTime.UtcNow.Ticks}.csv");
        }

        private class Employee
        {
            [Name("D")]
            public int Id { get; set; }

            [Name("N")]
            public string Name { get; set; }

            [Name("B")]
            public DateTime Birthday { get; set; }
        }
    }
}
