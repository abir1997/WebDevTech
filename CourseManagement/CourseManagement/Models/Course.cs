using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
    }
}
