using MovieRecommendation.BLL.Entities;

namespace MovieRecommendation.BLL.Services.Interfaces;

/// <summary>
///     Represents a repository for managing movies.
/// </summary>
public interface IMovieRepository : IDataRepository<Movie>
{
    /// <summary>
    ///     Gets a movie by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the movie.</param>
    /// <returns>The movie, or null if not found.</returns>
    Task<Movie?> GetByIdAsync(int id);
}