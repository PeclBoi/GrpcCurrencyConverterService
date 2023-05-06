namespace GrpcCurrencyConverterService
{
    public class CurrencyConverter
    {
        public double Convert(double amount, double fromRate, double toRate)
        {

            if (fromRate <= 0 || toRate <= 0)
            {
                throw new InvalidDataException("The rate cannot be 0 or negative.");
            }

            var exchamgeRate = toRate / fromRate;

            return Math.Floor(exchamgeRate * amount * 10000) / 10000;
        }

    }
}
