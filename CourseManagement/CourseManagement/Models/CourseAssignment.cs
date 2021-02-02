﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class CourseAssignment
    {
        public int InstructorID { get; set; }
        public virtual Instructor Instructor { get; set; }

        public int CourseID { get; set; }
        public virtual Course Course { get; set; }
    }
}
