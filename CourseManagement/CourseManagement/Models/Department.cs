using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50 ,MinimumLength =7)]
        public string Name { get; set; }
        public decimal Budget { get; set; }

        public DateTime StartDate { get; set; }
        public int InstructorID { get; set; }

        public virtual IEnumerable<Course> Courses { get; set; }
    }
}
