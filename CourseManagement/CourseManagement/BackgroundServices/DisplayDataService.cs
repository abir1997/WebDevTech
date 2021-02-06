using CourseManagement.Data;
using CourseManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourseManagement.BackgroundServices
{
    public class DisplayDataService : BackgroundService
    {

        private readonly ILogger<DisplayDataService> _logger;
        private readonly IServiceProvider _services;

        public DisplayDataService(IServiceProvider services, ILogger<DisplayDataService> logger)
        {
            _services = services;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DisplayData service is running");
            const int waitTime = 10;
            while (!cancellationToken.IsCancellationRequested)
            {
                await DoWork(cancellationToken);

                await Task.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken);
            }
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Fetching Data....");

            using var scope = _services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AzureContext>();
            var courses = await context.Courses.ToListAsync();
            var instructors = await context.Instructors.ToListAsync();

            DisplayCourseData(courses);
            DisplayInstructorData(instructors);
        }

        private void DisplayCourseData(List<Course> courses)
        {
            if (courses.Count() == 0)
            {
                _logger.LogInformation("No course data available");
                return;
            }
            const string format = "{0,-10} | {1,-15} | {2,-10} | {3}";
            Console.WriteLine(format, "CourseID", "Title", "Credits", "DepartmentID");
            Console.WriteLine(new string('-', 100));

            foreach (var course in courses)
            {
                Console.WriteLine(format, course.CourseID, course.Title, course.Credits, course.DepartmentID);
            }
        }

        private void DisplayInstructorData(List<Instructor> instructors)
        {
            if (instructors.Count() == 0)
            {
                _logger.LogInformation("No instructor data available");
                return;
            }
            const string format = "{0,-10} | {1,-15} | {2,-10} | {3}";
            Console.WriteLine(format, "InstructorID", "LastName", "FirstMidName", "HireDate");
            Console.WriteLine(new string('-', 100));

            foreach (var instructor in instructors)
            {
                Console.WriteLine(format, instructor.InstructorID, instructor.LastName, instructor.FirstMidName, instructor.HireDate);
            }
        }
    }
}
