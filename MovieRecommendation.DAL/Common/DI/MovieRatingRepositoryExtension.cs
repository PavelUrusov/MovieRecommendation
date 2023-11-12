using Microsoft.Extensions.DependencyInjection;
using MovieRecommendation.BLL.Common.Data;
using MovieRecommendation.BLL.Services.Interfaces;
using MovieRecommendation.DAL.Repositories;

namespace MovieRecommendation.DAL.Common.DI;

/// <summary>
///     Extension class for configuring movie rating repositories.
/// </summary>
public static class MovieRatingRepositoryExtension
{
    /// <summary>
    ///     Adds the movie rating repository to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddMovieRatingRepository(this IServiceCollection services)
    {
        services.AddSingleton<IMovieRatingRepository, MovieRatingRepository>(provider =>
            new MovieRatingRepository(FilePaths.MovieRating));

        return services;
    }
}