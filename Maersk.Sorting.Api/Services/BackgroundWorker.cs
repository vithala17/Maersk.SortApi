using Maersk.Sorting.Api.Data_Layer;
using Maersk.Sorting.Api.DataLayer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api.Services
{
    public class BackgroundWorker : BackgroundService
    {
        IServiceProvider serviceProvider;
        //IDBUtilities dBUtilities;
        ILogger<BackgroundWorker> logger;

        public BackgroundWorker(ILogger<BackgroundWorker> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            //this.dBUtilities = dBUtilities;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var hostedBbUtilities = (IDBUtilities)scope.ServiceProvider.GetRequiredService(typeof(IDBUtilities));


                    List<SortJob> allPendingSortJobs = (await hostedBbUtilities.GetSortJobs()).FindAll(x => x.Status == SortJobStatus.Pending);

                    this.logger.LogInformation("Background Worker ran at '{0}'. There are '{1}' pending sort jobs", DateTime.Today.TimeOfDay, allPendingSortJobs.Count);

                    foreach (SortJob job in allPendingSortJobs)
                    {
                        this.logger.LogInformation("Processing job with ID '{JobId}'.", job.Id);

                        var stopwatch = Stopwatch.StartNew();
                        var output = job.Input.OrderBy(n => n).ToArray();
                        var duration = stopwatch.Elapsed;

                        SortJob sortJob = new SortJob(
                        id: job.Id,
                        status: SortJobStatus.Completed,
                        duration: duration,
                        input: job.Input,
                        output: output);

                        this.logger.LogInformation("Completed processing job with ID '{JobId}'. Duration: '{Duration}'.", job.Id, duration);

                        await hostedBbUtilities.WriteToDb(sortJob);
                    }
                    Thread.Sleep(60000);
                }
            }
        }
    }
}
