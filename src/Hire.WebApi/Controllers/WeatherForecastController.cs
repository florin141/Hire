using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hire.Core.Data;
using Hire.Core.Domain;
using Hire.Core.Domain.Customers;
using Hire.Services.Converter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hire.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICurrencyConverter _currencyConverter;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepository<Customer> customerRepository, ICurrencyConverter currencyConverter)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _currencyConverter = currencyConverter;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var c = _currencyConverter.Convert(Currency.RON, Currency.EUR, 100).Result;

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
