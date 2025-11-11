using System.Net;
using System.Net.Http.Headers;

namespace RevenueCat.NET.Configuration;

internal static class HttpClientFactory
{
    public static HttpClient Create(RevenueCatOptions options)
    {
        var handler = new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2),
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1),
            MaxConnectionsPerServer = 10,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        var client = new HttpClient(handler)
        {
            BaseAddress = new Uri(options.BaseUrl),
            Timeout = options.Timeout
        };

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.ApiKey);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.UserAgent.ParseAdd("RevenueCat.NET/2.0.1");

        return client;
    }
}
