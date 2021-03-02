using Microsoft.Extensions.Configuration;
using System.IO;

namespace Maersk.Sorting.Api.Models
{
    public static class Constants
    {
        private static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        public static string ConnectionString => configuration["ConnectionStrings:DefaultConnection"];
    }
}
