using System;
using System.Globalization;
using System.Threading.Tasks;
using Hire.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace Hire.Services.Converter
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private const string BaseUrl = "https://api.apilayer.com/currency_data/";
        private const string ApiKey = "cEXwwvqLLdIFVL1Pnf9kzuXhFutxa7Co";

        private readonly ILogger<CurrencyConverter> _logger;

        public CurrencyConverter(ILogger<CurrencyConverter> logger)
        {
            _logger = logger;
        }

        public async Task<ConvertResult> Convert(string to, string from, string amount)
        {
            Guard.NotNull(to, nameof(to));
            Guard.NotNull(from, nameof(from));
            Guard.NotNull(amount, nameof(amount));

            var client = new RestClient(BaseUrl);

            var request = new RestRequest($"convert?to={to}&from={from}&amount={amount}");
            request.AddHeader("apikey", ApiKey);

            var response = await client.ExecuteAsync(request);

            var content = response.Content;
            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(content))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<ConvertResult>(content);
        }

        public async Task<float> Convert(Currency to, Currency from, int amount)
        {
            var result =  await Convert(Enum.GetName(typeof(Currency), to), Enum.GetName(typeof(Currency), from), amount.ToString(CultureInfo.InvariantCulture));

            if (result != null && result.Success)
            {
                return result.Result;
            }

            return default;
        }
    }
}
