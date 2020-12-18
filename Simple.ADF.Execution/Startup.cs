using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simple.ADF;
using Simple.ADF.Adapters;

[assembly: WebJobsStartup(typeof(Startup))]
namespace Simple.ADF
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddHttpClient<IPublicApiClient, PublicApiClient>((serviceProvider, client) =>
            {
                IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
                client.BaseAddress = new Uri(configuration["PUBLIC_API_BASE_URL"]);
            });
        }
    }
}
