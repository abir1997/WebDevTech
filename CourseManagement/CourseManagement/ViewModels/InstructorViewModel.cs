using CourseManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.ViewModels
{
    public class InstructorViewModel
    {
        public ICollection<Instructor> Instructors { get; set; }
    }
}
