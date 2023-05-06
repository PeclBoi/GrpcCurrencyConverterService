namespace GrpcCurrencyConverterService
{
    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public HttpResponseMessage Get(string requestUri)
        {
            return _httpClient.GetAsync(requestUri).Result;
        }
    }
}
