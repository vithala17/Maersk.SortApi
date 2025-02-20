﻿using Maersk.Sorting.Api.DataLayer;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api
{
    public class SortJobProcessor : ISortJobProcessor
    {
        private IDBUtilities dBUtilities;
        private readonly ILogger<SortJobProcessor> _logger;

        public SortJobProcessor(ILogger<SortJobProcessor> logger, IDBUtilities dBUtilities)
        {
            this._logger = logger;
            this.dBUtilities = dBUtilities;
        }

        public async Task<SortJob> Process(SortJob job)
        {
            _logger.LogInformation("Processing job with ID '{JobId}'.", job.Id);

            var stopwatch = Stopwatch.StartNew();

            var output = job.Input.OrderBy(n => n).ToArray();
            await Task.Delay(5000); // NOTE: This is just to simulate a more expensive operation

            var duration = stopwatch.Elapsed;

            _logger.LogInformation("Completed processing job with ID '{JobId}'. Duration: '{Duration}'.", job.Id, duration);

            return new SortJob(
                id: job.Id,
                status: SortJobStatus.Completed,
                duration: duration,
                input: job.Input,
                output: output);
        }
    
        public async Task ProcessAsync(SortJob job)
        {
            await this.dBUtilities.WriteToDb(job);
        }

        public async Task<SortJob[]> GetSortJobs()
        {
            List<SortJob> listOfJobs = await dBUtilities.GetSortJobs();

            SortJob[] arrayJobs = listOfJobs.ToArray<SortJob>();

            return arrayJobs;
        }

        public Task<SortJob> GetSortJob(string id)
        {
            var job = dBUtilities.GetSortJob(id);

            return job;
        }
    }
}
