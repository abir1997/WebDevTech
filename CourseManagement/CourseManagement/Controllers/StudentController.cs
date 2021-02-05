using CourseManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly AzureContext _context;

        public StudentController(AzureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync().ConfigureAwait(false);
            return View(students);
        }

        //public async Task<IActionResult> ViewStudents(int courseID)
        //{
        //    var course = await _context.Courses.FindAsync(courseID);
        //    if(course is null)
        //    {
        //        return NotFound();
        //    }
        //}
    }
}
