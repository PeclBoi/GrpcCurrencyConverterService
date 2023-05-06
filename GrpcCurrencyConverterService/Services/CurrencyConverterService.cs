using Grpc.Core;
using GrpcCurrencyConverterService.Protos;

namespace GrpcCurrencyConverterService.Services
{
    public class CurrencyConverterService : GrpcCurrencyConverterService.Protos.Converter.ConverterBase
    {

        private readonly ILogger<CurrencyConverterService> _logger;

        public CurrencyConverterService(ILogger<CurrencyConverterService> logger)
        {
            _logger = logger;
        }

        public override Task<ConvertReply> Converter(ConvertRequest request, ServerCallContext context)
        {

            IHttpClient httpClient = new HttpClientWrapper();
            CurrencyConverter currencyConverter = new CurrencyConverter();
            ReferenceRateProvider referenceRateProvider = new ReferenceRateProvider(httpClient);

            double fromRate = 0;
            double toRate = 0;

            fromRate = referenceRateProvider.GetRateForCurrency(request.From);
            toRate = referenceRateProvider.GetRateForCurrency(request.To);

            return Task.FromResult(new ConvertReply
            {
                Result = currencyConverter.Convert(request.Amount, fromRate, toRate)
            });
        }

    }
}
