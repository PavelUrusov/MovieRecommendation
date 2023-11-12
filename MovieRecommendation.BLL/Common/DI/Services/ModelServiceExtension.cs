using Microsoft.Extensions.DependencyInjection;
using MovieRecommendation.BLL.Services.Implementations;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.BLL.Common.DI.Services;

/// <summary>
///     Extension class for configuring model services.
/// </summary>
public static class ModelServiceExtension
{
    /// <summary>
    ///     Adds the model service to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddModelService(this IServiceCollection services)
    {
        services.AddSingleton<IModelService, ModelService>();
        return services;
    }
}