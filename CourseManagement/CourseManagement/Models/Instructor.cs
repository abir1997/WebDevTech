using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class Instructor
    {
        public int ID { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        public virtual IEnumerable<CourseAssignment> CourseAssignments { get; set; }

        public OfficeAssignment OfficeAssignment { get; set; }
    }
}
