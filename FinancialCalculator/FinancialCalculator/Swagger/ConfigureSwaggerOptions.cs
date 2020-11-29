using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialCalculator.Swagger
{
    public class ConfigureSwaggerOptions : ConfigureSwaggerOptionsBase
    {
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : base(provider)
        {
        }

        public override string XmlFileName => "Financial.Calculator.API.xml";

        public override string SwaggerPageTitle => "Financial Calculator API";
    }
}
