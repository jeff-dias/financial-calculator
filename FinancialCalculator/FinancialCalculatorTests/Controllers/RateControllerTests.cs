using FinancialCalculator.Controllers;
using FinancialCalculator.StaticFiles;
using Fms.Shared.Tests.Attributes;
using Xunit;

namespace FinancialCalculatorTests.Controllers
{
    public class RateControllerTests
    {
        [CustomFact]
        public void Should_return_rate_value_successful()
        {
            //Arrange
            var controller = new RateController();

            //Act
            var response = controller.Get();

            //Assert
            Assert.Equal(RateConstants.RateBase, response);

            //Dispose
            controller.Dispose();
        }
    }
}
