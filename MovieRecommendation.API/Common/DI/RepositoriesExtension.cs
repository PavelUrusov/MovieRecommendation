using MovieRecommendation.DAL.Common.DI;

namespace MovieRecommendation.API.Common.DI;

/// <summary>
///     Extension methods for configuring repositories.
/// </summary>
public static class RepositoriesExtension
{
    /// <summary>
    ///     Adds repositories to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> instance.</param>
    /// <returns>The configured <see cref="IServiceCollection" /> instance.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddMovieRatingRepository();
        services.AddMovieRepository();
        services.AddMovieLinkRepository();

        return services;
    }
}