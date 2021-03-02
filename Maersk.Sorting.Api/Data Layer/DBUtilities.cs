using Maersk.Sorting.Api.Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api.DataLayer
{
    public class DBUtilities : IDBUtilities
    {
        public ApplicationDBContext _context;

        public DBUtilities(ApplicationDBContext _context)
        {
            this._context = _context;
        }

        public Task WriteToDb(SortJob job)
        {
            var sortj = _context.SortJobs.ToList().Find(x => x.id == job.Id.ToString());
            if (sortj != null)
            {
                sortj.id = job.Id.ToString();
                sortj.status = job.Status.ToString();
                sortj.input = string.Join(',', job.Input);
                sortj.output = job.Output != null ? string.Join(',', job.Output) : "";
                sortj.duration = job.Duration.HasValue ? job.Duration.Value.Ticks : 0;

                _context.SortJobs.Update(sortj);
            }
            else
            {
                SortJobEntity sortJobEntity = new SortJobEntity(job);
                _context.Add(sortJobEntity);
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<List<SortJob>> GetSortJobs()
        {
            List<SortJob> allsortJobs = new List<SortJob>();
            var allJobs = _context.SortJobs.ToList();
            foreach (var job in allJobs)
            {
                allsortJobs.Add(ExtractSortJobFromSortJobEntity(job));
            }
            return Task.FromResult(allsortJobs);
        }

        public Task<SortJob> GetSortJob(string id)
        {
            SortJobEntity result = _context.SortJobs.SingleOrDefault(x => x.id == id);
            SortJob job= ExtractSortJobFromSortJobEntity(result);

            return Task.FromResult(job);
        }

        public static SortJob ExtractSortJobFromSortJobEntity(SortJobEntity result)
        {
            SortJob job;
            if (result != null)
            {
                Guid jobId = new Guid(result.id);
                SortJobStatus status = default!;

                if (result.status == "Pending")
                    status = SortJobStatus.Pending;
                else
                    status = SortJobStatus.Completed;

                TimeSpan duration = new TimeSpan(result.duration);

                int[] input = Array.ConvertAll(result.input.Split(','), value => Convert.ToInt32(value));

                int[]? output = default!;

                if (!String.IsNullOrEmpty(result.output))
                    output = Array.ConvertAll(result.output.Split(','), value => Convert.ToInt32(value));
                else
                    output = null;

                job = new SortJob(jobId, status, duration, input, output);
            }
            else
                job = default!;
            return job;
        }
    }
}
