using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Microsoft.Extensions.DependencyInjection
{
    public class SwaggerVersioningOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated = apiDescription.IsDeprecated();

            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                
                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                var routeInfo = description.RouteInfo;
                if (routeInfo == null)
                {
                    continue;
                }

                parameter.Required |= description.IsRequired;
            }

            // Overwrite description for common response codes
            var statusBadRequest = StatusCodes.Status400BadRequest.ToString(CultureInfo.InvariantCulture);
            if (operation.Responses.ContainsKey(statusBadRequest))
            {
                operation.Responses[statusBadRequest].Description = "Invalid query parameter(s). Read the response description";
            }

            var statusUnauthorized = StatusCodes.Status401Unauthorized.ToString(CultureInfo.InvariantCulture);
            if (operation.Responses.ContainsKey(statusUnauthorized))
            {
                operation.Responses[statusUnauthorized].Description = "Authorization has been denied for this request";
            }
        }
    }
}
