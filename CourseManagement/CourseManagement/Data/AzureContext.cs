using CourseManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Data
{
    public class AzureContext : DbContext
    {
        public AzureContext(DbContextOptions<AzureContext> options) : base(options)
        { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 1-* relationship
            //builder.Entity<Course>()
            //    .HasOne(c => c.Department)
            //    .WithMany(d => d.Courses);

            // Composite key
            builder.Entity<CourseAssignment>().HasKey(c => new { c.CourseID, c.InstructorID });
        }
    }
}
