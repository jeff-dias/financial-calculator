using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public abstract class ConfigureSwaggerOptionsBase : IConfigureOptions<SwaggerGenOptions>
    {
        public abstract string XmlFileName { get; }
        public abstract string SwaggerPageTitle { get; }

        private readonly IApiVersionDescriptionProvider _provider;

        protected ConfigureSwaggerOptionsBase(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            // Inclui para cada versão a documentação
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, SwaggerPageTitle));
            }

            options.OperationFilter<SwaggerVersioningOperationFilter>();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, XmlFileName));
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, string title)
        {
            var info = new OpenApiInfo()
            {
                Title = title,
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
