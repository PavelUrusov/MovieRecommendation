using MovieRecommendation.BLL.Entities;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.DAL.Repositories;

/// <summary>
///     Represents a repository for movie ratings stored in a CSV file.
/// </summary>
internal class MovieRatingRepository : CsvRepositoryBase<MovieRating>, IMovieRatingRepository
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MovieRatingRepository" /> class with the specified file path.
    /// </summary>
    /// <param name="filePath">The path to the CSV file.</param>
    public MovieRatingRepository(string filePath) : base(filePath)
    {
    }

    /// <summary>
    ///     Gets a movie rating by movie ID and user ID asynchronously.
    /// </summary>
    /// <param name="movieId">The ID of the movie.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The movie rating, or null if not found.</returns>
    public async Task<MovieRating?> GetAsync(int movieId, int userId)
    {
        var repository = await Repository.Value;
        var movieRating = repository.Find(x => x.MovieId == movieId && x.UserId == userId);

        return movieRating;
    }
}