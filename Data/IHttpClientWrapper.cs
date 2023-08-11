using System.Net.Http;
using System.Threading.Tasks;

namespace tmdb_web_application.Data
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
