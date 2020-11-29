using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class VersioningExtensions
    {
        /// <summary>
        /// Method to configure Swagger in API
        /// </summary>
        /// <typeparam name="TSwaggerOptions">ConfigureSwaggerOptions with specifics API parameters</typeparam>
        /// <param name="services">IServiceCollection to add Swagger API versioning</param>
        /// <param name="defaultApiVersion">API version used like default by Swagger</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerApiVersioning<TSwaggerOptions>(this IServiceCollection services, ApiVersion defaultApiVersion) where TSwaggerOptions : ConfigureSwaggerOptionsBase
        {
            services.AddApiVersioning(options =>
            {
                options.UseApiBehavior = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = defaultApiVersion;

                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen();
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, TSwaggerOptions>();

            return services;
        }

        /// <summary>
        /// Method to use Swagger in API
        /// </summary>
        /// <param name="app">IApplicationBuilder to use Swagger API versioning</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerApiVersioning(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }
    }
}
