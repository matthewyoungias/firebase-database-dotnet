using System.Net.Http;
using System.Net;
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Firebase.Database.Tests")]

namespace Firebase.Database
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Firebase.Database.Offline;
    using Firebase.Database.Query;

    /// <summary>
    /// Firebase client which acts as an entry point to the online database.
    /// </summary>
    public class FirebaseClient : IDisposable
    {
        internal readonly IHttpClientProxy HttpClient;
        internal readonly FirebaseOptions Options;

        private readonly string baseUrl;
        
        internal readonly WebProxy prox;
        internal readonly bool isProxy = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirebaseClient"/> class.
        /// </summary>
        /// <param name="baseUrl"> The base url. </param>
        /// <param name="options"> The Firebase options. </param>  
        /// <param name="prox"> Web Proxy settings. </param>
        
        public FirebaseClient(WebProxy Prox, string baseUrl, FirebaseOptions options = null)
        {
            if (prox != null)
            {
                isProxy = true;
                prox = Prox;

            }
            else
            {
                isProxy = false;
                prox = null;
            }
            this.Options = options ?? new FirebaseOptions();
            this.HttpClient = Options.HttpClientFactory.GetHttpClient(null, prox);
            
            
            this.baseUrl = baseUrl;

            if (!this.baseUrl.EndsWith("/"))
            {
                this.baseUrl += "/";
            }

            this.isProxy = isProxy;
        }

        /// <summary>
        /// Queries for a child of the data root.
        /// </summary>
        /// <param name="resourceName"> Name of the child. </param>
        /// <returns> <see cref="ChildQuery"/>. </returns>
        public ChildQuery Child(string resourceName)
        {
            return new ChildQuery(this, () => this.baseUrl + resourceName);
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
        }
    }
}
