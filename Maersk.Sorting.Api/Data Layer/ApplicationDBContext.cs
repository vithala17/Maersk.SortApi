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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<SortJobEntity>(
        //        b =>
        //        {
        //            b.HasKey("id");
        //            b.Property(e => e.input);
        //            b.Property(e => e.output);
        //            b.Property(e => e.status);
        //            b.Property(e => e.duration);
        //        });
        //}
    }

    public class SortJobEntity
    {
        public SortJobEntity()
        {
            id = default!;
            input = default!;
            output = default!;
            status = default!;
            duration = duration!;
        }

        public SortJobEntity(SortJob sortjob)
        {
            id = sortjob.Id.ToString();

            input = string.Join(',', sortjob.Input);

            if (sortjob.Output != null)
                output = string.Join(',', sortjob.Output);
            else
                output = string.Empty;

            status = sortjob.Status.ToString();

            if (sortjob.Duration.HasValue)
                duration = sortjob.Duration.Value.Ticks;
        }

        //public SortJobEntity(Guid Id, int[] Input, int[] Output, SortJobStatus Status, TimeSpan Duration)
        //{
        //    id = Id.ToString();

        //    input = string.Join(',', Input);

        //    if (output != null)
        //        output = string.Join(',', Output);
        //    else
        //        output = string.Empty;

        //    status = Status.ToString();

        //    duration = Duration.Ticks;
        //}

        public string id { get; set; }

        public string input { get; set; }

        public string output { get; set; }

        public string status { get; set; }

        public long duration { get; set; }
    }
}
