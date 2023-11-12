using MovieRecommendation.BLL.Entities;

namespace MovieRecommendation.BLL.Services.Interfaces;

/// <summary>
///     Represents a service for managing movie ratings.
/// </summary>
public interface IMovieRatingService
{
    /// <summary>
    ///     Gets a movie rating asynchronously by movie ID and user ID.
    /// </summary>
    /// <param name="movieId">The ID of the movie.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The movie rating, or null if not found.</returns>
    Task<MovieRating?> GetAsync(int movieId, int userId);

    /// <summary>
    ///     Gets a list of movie ratings asynchronously.
    /// </summary>
    /// <returns>The list of movie ratings.</returns>
    Task<List<MovieRating>> GetListAsync();

    /// <summary>
    ///     Gets a list of all watched movies by a user asynchronously.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The list of watched movies.</returns>
    Task<List<MovieRating>> GetAllWatchedMoviesByUserAsync(int userId);

    /// <summary>
    ///     Gets a list of the top 100 rated movies.
    /// </summary>
    /// <returns>List of the best films rated by users</returns>
    List<BestMovie> GetBestMovies();
}