using Microsoft.Extensions.DependencyInjection;
using MovieRecommendation.BLL.Services.Implementations;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.BLL.Common.DI.Services;

/// <summary>
///     Extension class for configuring movie rating services.
/// </summary>
public static class MovieRatingServiceExtension
{
    /// <summary>
    ///     Adds the movie rating service to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddMovieRatingService(this IServiceCollection services)
    {
        services.AddScoped<IMovieRatingService, MovieRatingService>();
        return services;
    }
}