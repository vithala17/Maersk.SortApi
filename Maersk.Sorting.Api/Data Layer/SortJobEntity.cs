namespace Maersk.Sorting.Api.Data_Layer
{
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

        public string id { get; set; }

        public string input { get; set; }

        public string output { get; set; }

        public string status { get; set; }

        public long duration { get; set; }
    }
}
