using CourseManagement.Data;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace CourseManagement.Controllers
{
    public class InstructorController : Controller
    {
        private readonly AzureContext _context;
        
        public InstructorController(AzureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var instructors = await _context.Instructors.ToListAsync().ConfigureAwait(false);

            return View(new InstructorViewModel
            {
                Instructors = await instructors.ToPagedListAsync(1, 4).ConfigureAwait(false)
            });
        }
    }
}
