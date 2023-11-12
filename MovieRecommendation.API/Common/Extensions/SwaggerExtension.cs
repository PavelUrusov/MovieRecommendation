using System.Reflection;
using Microsoft.OpenApi.Models;

namespace MovieRecommendation.API.Common.Extensions;

/// <summary>
///     Extension methods for configuring Swagger documentation.
/// </summary>
public static class SwaggerExtension
{
    /// <summary>
    ///     Adds Swagger documentation services to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> instance.</param>
    /// <returns>The configured <see cref="IServiceCollection" /> instance.</returns>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie Recommendation API", Version = "v1" });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}