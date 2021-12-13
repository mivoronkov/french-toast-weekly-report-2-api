using Microsoft.Extensions.Configuration;
using System.IO;

namespace CM.WeeklyTeamReport.Domain.IntegrationTests
{
    class SetupTests
    {
        public static IConfiguration Configuration => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(
                 path: "appsettings.Development.json",
                 optional: false,
                 reloadOnChange: true)
           .Build();
    }
}
