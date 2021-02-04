using CourseManagement.Data;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly AzureContext _context;

        public CourseController(AzureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync().ConfigureAwait(false);
            return View(new CourseViewModel
            {
                Courses = courses
            });
        }
    }
}
