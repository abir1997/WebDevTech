using CourseManagement.Data;
using CourseManagement.Models;
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);

            if (instructor is null)
            {
                return NotFound();
            }

            return View(instructor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstructorID, LastName, FirstMidName, HireDate")] Instructor instructor)
        {
            if(id != instructor.InstructorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_context.Instructors.Any(x => x.InstructorID == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(instructor);
        }

    }
}
