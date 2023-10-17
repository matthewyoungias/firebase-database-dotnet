using System;
using System.Net;

namespace Firebase
{
    public interface IHttpClientFactory
    {
        IHttpClientProxy GetHttpClient(TimeSpan? timeout, WebProxy prox);
    }
}
