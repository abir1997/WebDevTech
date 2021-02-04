﻿using CourseManagement.Data;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    public class InstructorController : Controller
    {
        private readonly AzureContext _context;
        
        public InstructorController(AzureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var instructors = await _context.Instructors.ToListAsync().ConfigureAwait(false);

            return View(new InstructorViewModel
            {
                Instructors = instructors
            });
        }
    }
}