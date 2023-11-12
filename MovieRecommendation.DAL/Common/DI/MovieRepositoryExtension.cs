using Microsoft.Extensions.DependencyInjection;
using MovieRecommendation.BLL.Common.Data;
using MovieRecommendation.BLL.Services.Interfaces;
using MovieRecommendation.DAL.Repositories;

namespace MovieRecommendation.DAL.Common.DI;

/// <summary>
///     Extension class for configuring movie repositories.
/// </summary>
public static class MovieRepositoryExtension
{
    /// <summary>
    ///     Adds the movie repository to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddMovieRepository(this IServiceCollection services)
    {
        services.AddSingleton<IMovieRepository, MovieRepository>(provider =>
            new MovieRepository(FilePaths.Movies));

        return services;
    }
}