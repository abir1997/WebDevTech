using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public enum Grade
    {
        A = 1,
        B,
        C,
        D,
        E,
        F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }

        public Grade? Grade { get; set; }
    }
}
