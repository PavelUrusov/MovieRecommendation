using Microsoft.Extensions.DependencyInjection;
using MovieRecommendation.BLL.Common.Data;
using MovieRecommendation.BLL.Services.Interfaces;
using MovieRecommendation.DAL.Repositories;

namespace MovieRecommendation.DAL.Common.DI;

/// <summary>
///     Extension class for configuring movie link repositories.
/// </summary>
public static class MovieLinkRepositoryExtension
{
    /// <summary>
    ///     Adds the movie link repository to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddMovieLinkRepository(this IServiceCollection services)
    {
        services.AddSingleton<IMovieLinkRepository, MovieLinkRepository>(provider =>
            new MovieLinkRepository(FilePaths.MovieLinks));

        return services;
    }
}