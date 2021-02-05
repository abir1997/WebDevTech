using CourseManagement.Data;
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
            _logger.LogInformation("Fetching Data....");
            const int waitTime = 10;
            while (!cancellationToken.IsCancellationRequested)
            {
                await DoWork(cancellationToken);

                _logger.LogInformation("DisplayData service is waiting.");

                await Task.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken);
            }
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation("DisplayData service is working");

            using var scope = _services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AzureContext>();
            var courses = await context.Courses.ToListAsync();
            var instructors = await context.Instructors.ToListAsync();

            // Output info.
        }
    }
}
