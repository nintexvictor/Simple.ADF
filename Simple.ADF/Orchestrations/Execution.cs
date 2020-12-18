using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Simple.ADF.Entities;
using Simple.ADF.Models;

namespace Simple.ADF
{
    public class Execution
    {
        private ILogger _logger;

        public Execution(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Execution>();
        }

        [FunctionName(nameof(Execution))]
        public async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            ExecutionInput input = context.GetInput<ExecutionInput>();

            string entries = await context.CallActivityAsync<string>(
                nameof(GetEntries),
                new GetEntriesInput
                {
                    Category = input.Category
                });

            CallCounter.Increment(context, nameof(GetEntries));

            _logger.LogInformation(entries);
        }
    }
}