using MovieRecommendation.BLL.Entities;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.DAL.Repositories;

/// <summary>
///     Represents a repository for managing movie links.
/// </summary>
internal class MovieLinkRepository : CsvRepositoryBase<MovieLink>, IMovieLinkRepository
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MovieLinkRepository" /> class.
    /// </summary>
    /// <param name="filePath">The file path of the repository.</param>
    public MovieLinkRepository(string filePath) : base(filePath)
    {
    }

    private string BaseLink => "https://www.imdb.com/title/tt0";

    /// <summary>
    ///     Gets a movie link by its ID.
    /// </summary>
    /// <param name="movieId">The ID of the movie to retrieve the link for.</param>
    /// <returns>A Task representing the asynchronous operation that returns the MovieLink object.</returns>
    public async Task<MovieLink?> GetById(int movieId)
    {
        var repository = await Repository.Value;
        var movieLink = repository.Find(x => x.MovieId == movieId);

        if (movieLink != null)
        {
            movieLink.ImdbLink = GenerateImdbLink(movieLink.ImdbId);
        }

        return movieLink;
    }

    /// <summary>
    ///     Generates the IMDb link for a given IMDb ID.
    /// </summary>
    /// <param name="imdbId">The IMDb ID of the movie.</param>
    /// <returns>The generated IMDb link.</returns>
    private string GenerateImdbLink(string imdbId)
    {
        var link = string.Concat(BaseLink, imdbId, "/");

        return link;
    }
}