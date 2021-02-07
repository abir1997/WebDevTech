using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using X.PagedList;

namespace CourseManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly AzureContext _context;
        private const string InstructorSessionKey = "_InstructorSessionKey";
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<CourseController> _logger;
        private HttpClient Client => _clientFactory.CreateClient("api");
        private const string Prefix = "api/course";
        public CourseController(IHttpClientFactory clientFactory, ILogger<CourseController> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //var courses = await _context.Courses.ToListAsync().ConfigureAwait(false);
            var response = await Client.GetAsync("api/course");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Unable to get any courses.");
            }
            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var courses = JsonConvert.DeserializeObject<List<Course>>(result);

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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseID, Title, Credits, DepartmentID")] Course course)
        {
            if(id != course.CourseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_context.Courses.Any(x => x.CourseID == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
    }
}
