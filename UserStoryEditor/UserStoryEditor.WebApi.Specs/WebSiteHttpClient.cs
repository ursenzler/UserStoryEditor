namespace UserStoryEditor.WebApi.Specs
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class WebsiteHttpClient : IDisposable
    {
        private readonly HttpClient httpClient;

        public WebsiteHttpClient(
            HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetFromApi(
            string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return await this.httpClient.SendAsync(request, CancellationToken.None);
        }

        public async Task<HttpResponseMessage> PostToApi(
            string json,
            string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            return await this.httpClient.SendAsync(request, CancellationToken.None);
        }

        public async Task<HttpResponseMessage> PutToApi(
            string json,
            string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            return await this.httpClient.SendAsync(request, CancellationToken.None);
        }

        public async Task<HttpResponseMessage> DeleteOnApi(
            string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            return await this.httpClient.SendAsync(request, CancellationToken.None);
        }

        public void Dispose()
        {
            this.httpClient.Dispose();
        }
    }
}