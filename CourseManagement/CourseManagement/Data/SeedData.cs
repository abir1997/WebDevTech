using CourseManagement.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CourseManagement.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AzureContext>();

            if (context.Instructors.Any())
            {
                return;
            }

            context.Instructors.AddRange(
                new Instructor
                {
                    InstructorID = 2000,
                    LastName = "Kalra",
                    FirstMidName = "Shekhar",
                    HireDate = DateTime.UtcNow
                },
                new Instructor
                {
                    InstructorID = 2100,
                    LastName = "Bolger",
                    FirstMidName = "Matthew",
                    HireDate = DateTime.UtcNow
                });

            context.OfficeAssignments.AddRange(
                new OfficeAssignment
                {
                    InstructorID = 2000,
                    Location = "RMIT"
                },
                new OfficeAssignment { 
                    InstructorID = 2100,
                    Location = "Telstra"
                });

            context.Departments.AddRange(
                new Department
                {
                    DepartmentID = 123,
                    Name = "Science",
                    Budget = 250.60m,
                    StartDate = DateTime.UtcNow,
                    InstructorID = 2000
                },
                new Department
                {
                    DepartmentID = 666,
                    Name = "Devilry",
                    Budget = 666.00m,
                    StartDate = DateTime.UtcNow,
                    InstructorID = 2100
                });

            context.Courses.AddRange(
                new Course
                {
                    CourseID = 10,
                    Title = "WDT",
                    Credits = 2,
                    DepartmentID = 123
                }, 
                new Course
                {
                    CourseID = 11,
                    Title = "Spells",
                    Credits = 3,
                    DepartmentID = 666
                });

            context.CourseAssignments.AddRange(
                new CourseAssignment
                {
                    CourseID = 10,
                    InstructorID = 2000
                },
                new CourseAssignment
                {
                    CourseID = 11,
                    InstructorID = 2100
                });

            context.Students.AddRange(
                new Student
                {
                    StudentID = 1,
                    LastName = "Ishtiaque",
                    FirstMidName = "Abir",
                    EnrollmentDate = DateTime.UtcNow
                },
                new Student
                {
                    StudentID = 2,
                    LastName = "Hermoine",
                    FirstMidName = "Granger",
                    EnrollmentDate = DateTime.UtcNow
                });

            context.Enrollments.AddRange(
                new Enrollment
                {
                    CourseID = 10,
                    StudentID = 1,
                    Grade = Grade.A
                },
                new Enrollment
                {
                    CourseID = 11,
                    StudentID = 2,
                    Grade = Grade.B
                });

            context.SaveChanges();
        }
    }
}
