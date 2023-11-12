using MovieRecommendation.BLL.Entities;

namespace MovieRecommendation.BLL.Services.Interfaces;

/// <summary>
///     Represents a repository for managing movie ratings.
/// </summary>
public interface IMovieRatingRepository : IDataRepository<MovieRating>
{
    /// <summary>
    ///     Gets a movie rating asynchronously by movie ID and user ID.
    /// </summary>
    /// <param name="movieId">The ID of the movie.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The movie rating, or null if not found.</returns>
    Task<MovieRating?> GetAsync(int movieId, int userId);
}