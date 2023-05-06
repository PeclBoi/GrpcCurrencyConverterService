using System.Globalization;
using System.Net.Http;
using System.Xml;

namespace GrpcCurrencyConverterService
{
    public class ReferenceRateProvider
    {

        IHttpClient _httpClient;

        public ReferenceRateProvider(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Dictionary<string, double> GetRates()
        {
            string ECB_URL = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

            var response = _httpClient.Get(ECB_URL);
            var xmlString = response.Content.ReadAsStringAsync().Result;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;


            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlString), settings);

            // Find the Cube node with the exchange rates
            reader.ReadToFollowing("Cube");
            reader.ReadToFollowing("Cube");
            string time = reader.GetAttribute("time");
            // Find the Cube node with USD exchange rate

            Dictionary<string, double> rates = new Dictionary<string, double>();

            //reader.ReadToFollowing("Cube");
            while (reader.Read())
            {
                while (reader.MoveToNextAttribute())
                {
                    if (reader.Name == "currency")
                    {
                        var currency = reader.GetAttribute("currency");
                        var rate = reader.GetAttribute("rate");

                        if (currency != null && rate != null)
                        {
                            rates.Add(currency, Convert.ToDouble(rate, CultureInfo.InvariantCulture));
                        }
                    }
                }
            }

            return rates;
        }

        public double GetRateForCurrency(string currency)
        {

            if (currency == "EUR")
            {
                return 1;
            }

            try
            {
                return GetRates()[currency];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
