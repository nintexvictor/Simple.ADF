using System.Threading.Tasks;

namespace Simple.ADF.Adapters
{
    public interface IPublicApiClient
    {
        Task<string> GetEntries(string category = null);
    }
}