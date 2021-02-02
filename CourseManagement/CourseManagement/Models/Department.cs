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

        [StringLength(50, MinimumLength =7)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public int InstructorID { get; set; }
        public virtual Instructor Instructor { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
