using System.Net.Http;
using System.Threading.Tasks;

namespace Simple.ADF.Adapters
{
    public class PublicApiClient : IPublicApiClient
    {
        private readonly HttpClient _client;

        public PublicApiClient(HttpClient client)
        {
            this._client = client;
        }

        public async Task<string> GetEntries(string category = null)
        {
            string requestUrl = string.IsNullOrEmpty(category)
                ? "/entries"
                : $"/entries?category={category}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            HttpResponseMessage response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
