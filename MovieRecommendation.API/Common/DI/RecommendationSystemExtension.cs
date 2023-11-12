using MovieRecommendation.BLL.Common.DI.Services;

namespace MovieRecommendation.API.Common.DI;

/// <summary>
///     Extension methods for configuring recommendation system services.
/// </summary>
public static class RecommendationSystemExtension
{
    /// <summary>
    ///     Adds recommendation system services to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> instance.</param>
    /// <returns>The configured <see cref="IServiceCollection" /> instance.</returns>
    public static IServiceCollection AddRecommendationSystemServices(this IServiceCollection services)
    {
        services.AddRecommendationService();
        services.AddModelService();
        services.AddMachineLearningContext();
        services.AddMovieRatingService();
        services.AddMovieService();

        return services;
    }
}