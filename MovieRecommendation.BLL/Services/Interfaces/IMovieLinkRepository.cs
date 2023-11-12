using MovieRecommendation.BLL.Entities;

namespace MovieRecommendation.BLL.Services.Interfaces;

/// <summary>
///     Represents a repository for managing movie links.
/// </summary>
public interface IMovieLinkRepository : IDataRepository<MovieLink>
{
    /// <summary>
    ///     Gets a movie link by its ID.
    /// </summary>
    /// <param name="movieId">The ID of the movie to retrieve the link for.</param>
    /// <returns>A Task representing the asynchronous operation that returns the MovieLink object.</returns>
    Task<MovieLink?> GetById(int movieId);
}