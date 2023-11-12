using MovieRecommendation.BLL.Entities;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.DAL.Repositories;

/// <summary>
///     Represents a repository for movies stored in a CSV file.
/// </summary>
internal class MovieRepository : CsvRepositoryBase<Movie>, IMovieRepository
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MovieRepository" /> class with the specified file path.
    /// </summary>
    /// <param name="filePath">The path to the CSV file.</param>
    public MovieRepository(string filePath) : base(filePath)
    {
    }

    /// <summary>
    ///     Gets a movie by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the movie.</param>
    /// <returns>The movie, or null if not found.</returns>
    public async Task<Movie?> GetByIdAsync(int id)
    {
        var movies = await Repository.Value;
        var movie = movies.Find(x => x.Id == id);

        return movie;
    }
}