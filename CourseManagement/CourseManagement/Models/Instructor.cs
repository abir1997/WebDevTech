using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class Instructor
    {
        public int ID { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        public virtual ICollection<CourseAssignment> CourseAssignments { get; set; }

        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}
