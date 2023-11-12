using Microsoft.Extensions.DependencyInjection;
using MovieRecommendation.BLL.Services.Implementations;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.BLL.Common.DI.Services;

/// <summary>
///     Extension class for configuring movie services.
/// </summary>
public static class MovieServiceExtension
{
    /// <summary>
    ///     Adds the movie service to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddMovieService(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();

        return services;
    }
}