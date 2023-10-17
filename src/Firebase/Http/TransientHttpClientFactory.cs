using System;
using System.Net;
using System.Net.Http;

namespace Firebase
{
    internal sealed class TransientHttpClientFactory : IHttpClientFactory
    {
        public IHttpClientProxy GetHttpClient(TimeSpan? timeout, WebProxy prox)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.Proxy = prox;

            var client = new HttpClient(clientHandler);
            if (timeout != null) {
                client.Timeout = timeout.Value;
            }

            return new SimpleHttpClientProxy(client);
        }
    }
}
