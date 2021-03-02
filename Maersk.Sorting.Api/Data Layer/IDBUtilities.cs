using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api.DataLayer
{
    public interface IDBUtilities
    {
        Task WriteToDb(SortJob job);

        Task<SortJob> GetSortJob(string id);

        Task<List<SortJob>> GetSortJobs();
    }
}