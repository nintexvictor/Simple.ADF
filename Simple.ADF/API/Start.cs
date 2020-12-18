using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Simple.ADF.Models;

namespace Simple.ADF
{
    public class Start
    {
        private readonly ILogger _logger;

        public Start(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Start>();
        }

        [FunctionName(nameof(Start))]
        public async Task<IActionResult> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [DurableClient] IDurableClient client)
        {
            string category = (req.Query.TryGetValue("category", out StringValues categoryParams))
                ? categoryParams.ToString()
                : string.Empty;

            string instanceId = await client.StartNewAsync(
                nameof(Execution),
                new ExecutionInput
                {
                    Category = category
                });

            _logger.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return client.CreateCheckStatusResponse(req, instanceId);
        }
    }
}