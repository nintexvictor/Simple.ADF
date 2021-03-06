﻿using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;

namespace Simple.ADF.Entities
{
    /// <summary>
    /// Counts number of times an API has been invoked.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class CallCounter
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        public static void Increment(IDurableOrchestrationContext ctx, string counterName)
        {
            ctx.SignalEntity(new EntityId(nameof(CallCounter), counterName.ToLowerInvariant()), nameof(Increment));
        }

        public static async Task<int> GetCount(IDurableClient client, string counterName)
        {
            try
            {
                var result = await client.ReadEntityStateAsync<CallCounter>(new EntityId(nameof(CallCounter), counterName.ToLowerInvariant()));
                return result.EntityState?.Value ?? 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
