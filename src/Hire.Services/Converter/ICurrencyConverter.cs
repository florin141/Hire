using System.Threading.Tasks;

namespace Hire.Services.Converter
{
    public interface ICurrencyConverter
    {
        Task<ConvertResult> Convert(string to, string from, string amount);

        Task<float> Convert(Currency to, Currency from, int amount);
    }
}
