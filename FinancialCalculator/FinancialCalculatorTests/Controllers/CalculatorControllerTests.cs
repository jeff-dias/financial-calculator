using Bogus;
using FinancialCalculator.Controllers;
using FinancialCalculator.StaticFiles;
using Fms.Shared.Tests.Attributes;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace FinancialCalculatorTests.Controllers
{
    public class CalculatorControllerTests
    {
        private readonly Faker _faker;
        private readonly Mock<ILogger<CalculatorController>> _logger;

        public CalculatorControllerTests()
        {
            _faker = new Faker();
            _logger = new Mock<ILogger<CalculatorController>>();
        }

        [CustomFact]
        public void Should_calculate_rate_on_initial_value_correctly()
        {
            //Arrange
            var controller = new CalculatorController(_logger.Object);
            var initialValue = _faker.Random.Decimal(100, 10000);
            var period = _faker.Random.Int(1, 120);

            //Act
            var response = controller.GetCalculator(initialValue, period);

            //Assert
            var expectedValue = Math.Round(initialValue * Convert.ToDecimal(Math.Pow((double)(1 + RateConstants.RateBase), period)), 2, MidpointRounding.ToZero);
            Assert.Equal(expectedValue, response);

            //Dispose
            controller.Dispose();
        }

        [CustomFact]
        public void Should_return_git_hub_repository_url_successful()
        {
            //Arrange
            var controller = new CalculatorController(_logger.Object);

            //Act
            var response = controller.GetShowMeTheCode();

            //Assert
            Assert.Equal(RateConstants.GitHubRepositoryUrl, response);

            //Dispose
            controller.Dispose();
        }
    }
}
