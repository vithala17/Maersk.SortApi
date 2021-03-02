using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api
{
    public interface ISortJobProcessor
    {
        Task<SortJob> Process(SortJob job);

        Task ProcessAsync(SortJob job);

        Task<SortJob[]> GetSortJobs();

        Task<SortJob> GetSortJob(string id);
    }
}