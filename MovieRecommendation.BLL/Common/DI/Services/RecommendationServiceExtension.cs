using Microsoft.Extensions.DependencyInjection;
using MovieRecommendation.BLL.Services.Implementations;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.BLL.Common.DI.Services;

/// <summary>
///     Extension class for configuring recommendation services.
/// </summary>
public static class RecommendationServiceExtension
{
    /// <summary>
    ///     Adds the recommendation service to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddRecommendationService(this IServiceCollection services)
    {
        services.AddScoped<IRecommendationService, RecommendationService>();
        return services;
    }
}