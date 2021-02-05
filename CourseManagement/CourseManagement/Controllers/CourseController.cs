using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace CourseManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly AzureContext _context;
        private const string InstructorSessionKey = "_InstructorSessionKey";

        public CourseController(AzureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync().ConfigureAwait(false);
            return View(new CourseViewModel
            {
                Courses = await courses.ToPagedListAsync(1, 4).ConfigureAwait(false)
            });
        }

        public async Task<IActionResult> IndexToViewCourses(int instructorID)
        {
            var instructor = await _context.Instructors.FindAsync(instructorID);

            if (instructor is null)
            {
                return NotFound();
            }

            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            var instructorJson = JsonConvert.SerializeObject(instructor, Formatting.Indented, serializerSettings);

            HttpContext.Session.SetString(InstructorSessionKey, instructorJson);
            return RedirectToAction(nameof(ViewCourses));
        }

        public async Task<IActionResult> ViewCourses(int? page = 1)
        {
            const int pageSize = 4;
            var instructorJson = HttpContext.Session.GetString(InstructorSessionKey);
            if (instructorJson is null)
            {
                return RedirectToAction("Index", "Home");
            }

            var instructor = JsonConvert.DeserializeObject<Instructor>(instructorJson);
            var courseAssignments = instructor.CourseAssignments;
            var courseIds = new List<int>();
            foreach (var course in courseAssignments)
            {
                courseIds.Add(course.CourseID);
            }
            var courses = await _context.Courses.ToListAsync().ConfigureAwait(false);
            var filteredCourses = new List<Course>();
            foreach (var course in courses)
            {
                if (course != null && courseIds.Contains(course.CourseID))
                {
                    filteredCourses.Add(course);
                }
            }

            var pagedList = await filteredCourses
                .OrderByDescending(x => x.Title)
                .ToPagedListAsync(page.Value, pageSize).ConfigureAwait(false);

            return View(new CourseViewModel
            {
                Courses = pagedList
            });
        }
    }
}
