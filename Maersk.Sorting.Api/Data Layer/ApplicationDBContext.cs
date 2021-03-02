using Maersk.Sorting.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api.Data_Layer
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<SortJobEntity> SortJobs { get; set; } = default!;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
    }
}
