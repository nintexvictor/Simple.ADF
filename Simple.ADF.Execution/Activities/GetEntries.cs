using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Simple.ADF.Adapters;
using Simple.ADF.Models;
using System.Threading.Tasks;

namespace Simple.ADF
{
    public class GetEntries
    {
        private readonly IPublicApiClient _client;
        private readonly ILogger _logger;

        public GetEntries(IPublicApiClient client, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetEntries>();
            _client = client;
        }

        [FunctionName(nameof(GetEntries))]
        public async Task<string> Execute([ActivityTrigger] GetEntriesInput input)
        {
            return await _client.GetEntries(input.Category);
        }
    }
}
