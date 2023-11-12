using Microsoft.Extensions.DependencyInjection;
using Microsoft.ML;

namespace MovieRecommendation.BLL.Common.DI.Services;

/// <summary>
///     Extension class for configuring the machine learning context.
/// </summary>
public static class MachineLearningContextExtension
{
    /// <summary>
    ///     Adds the machine learning context to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddMachineLearningContext(this IServiceCollection services)
    {
        services.AddSingleton<MLContext>();
        return services;
    }
}