using MovieRecommendation.API.Common.DI;
using MovieRecommendation.API.Common.Helpers;

namespace MovieRecommendation.API.Common.Extensions;

/// <summary>
///     Extension methods for configuring services in the web application builder.
/// </summary>
public static class WebApplicationBuilderExtension
{
    /// <summary>
    ///     Configures the services in the web application builder.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder" /> instance.</param>
    /// <returns>The configured <see cref="WebApplicationBuilder" /> instance.</returns>
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRepositories();
        builder.Services.AddRecommendationSystemServices();
        builder.Services.AddControllers(options => { options.Filters.Add<GlobalExceptionFilter>(); });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerDocumentation();

        return builder;
    }
}