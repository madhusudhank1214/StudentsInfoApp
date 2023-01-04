using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsInfoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class StudentsController : ControllerBase
    {
        private static readonly string[] StudentNames = new[]
        {
            "Madhu", "Raju", "Rani", "Mithali", "Milky"
        };

        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<StudentsInfo> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new StudentsInfo
            {
                Date = DateTime.Now.AddDays(index),
                studentId = rng.Next(-20, 55),
                studentName = StudentNames[rng.Next(StudentNames.Length)]
            })
            .ToArray();
        }
        [HttpPost]
        public IEnumerable<StudentsInfo> SaveStudentsData([FromBody]StudentsInfo studentsInfo)
        {
            List<StudentsInfo> _lstStudents = new List<StudentsInfo>();
            //Read from a file
            //string students = System.IO.File.ReadAllText("C:\\StudentInfo\\Student_info.txt");

            //Write to a file
            using (StreamWriter writer = new StreamWriter("C:\\StudentInfo\\Student_info.txt", append: true))
            {
                writer.WriteLine(studentsInfo.studentId + "\t" + studentsInfo.studentName + System.Environment.NewLine);
                _lstStudents.Add(studentsInfo);
            }
            return _lstStudents;
        }
    }
}
