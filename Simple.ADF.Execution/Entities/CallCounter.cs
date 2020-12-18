using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;

namespace Simple.ADF.Entities
{
    /// <summary>
    /// Counts number of times an API has been invoked.
    /// </summary>
    public partial class CallCounter
    {
        public void Increment() => this.Value += 1;

        public int Count() => this.Value;

        [FunctionName(nameof(CallCounter))]
        public static Task Run([EntityTrigger] IDurableEntityContext ctx) => ctx.DispatchAsync<CallCounter>();
    }
}
