namespace GrpcCurrencyConverterService
{
    public interface IHttpClient
    {
        HttpResponseMessage Get(string requestUrl);
    }
}
