using FinancialCalculator.StaticFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace FinancialCalculator.Controllers
{
    [ApiController]
    public class CalculatorController : Controller
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route(ControllerConstants.GetCalculatorRoute)]
        public decimal GetCalculator(decimal valorInicial, int meses)
        {
            _logger.LogInformation($"Initial value receipt {valorInicial}");
            _logger.LogInformation($"Period value receipt {meses}");

            var calculatedValue = valorInicial * Convert.ToDecimal(Math.Pow((double)(1 + RateConstants.RateBase), meses));

            _logger.LogInformation($"Calculate value returned {calculatedValue}");

            return Math.Round(calculatedValue, 2, MidpointRounding.ToZero);
        }

        [HttpGet]
        [Route(ControllerConstants.GetShowMeTheCodeRoute)]
        public string GetShowMeTheCode()
        {
            return RateConstants.GitHubRepositoryUrl;
        }
    }
}