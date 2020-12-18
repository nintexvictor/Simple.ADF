using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Simple.ADF.Entities;
using Simple.ADF.Models;

namespace Simple.ADF.API
{
    /// <summary>
    /// API to retrieve the number of times an API has been invoked.
    /// </summary>
    public class Count
    {
        private readonly ILogger _logger;

        public Count(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Start>();
        }

        [FunctionName(nameof(Count))]
        public async Task<IActionResult> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Count/{category}")] HttpRequest req,
            [DurableClient(ConnectionName = "DurableFunctionsStorage", TaskHub = "SimpleADF")] IDurableClient client,
            string category)
        {
            _logger.LogInformation($"Getting invocation count on Public API.");

            return new OkObjectResult(
                new CountOutput
                {
                    Count = await CallCounter.GetCount(client, category)
                });
        }
    }
}
