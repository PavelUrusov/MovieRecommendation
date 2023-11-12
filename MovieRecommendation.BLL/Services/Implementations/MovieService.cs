using Microsoft.Extensions.Logging;
using MovieRecommendation.BLL.Entities;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.BLL.Services.Implementations;

/// <summary>
///     Represents a service for managing movies.
/// </summary>
internal class MovieService : IMovieService
{
    private readonly ILogger<MovieService> _logger;
    private readonly IMovieRepository _movieRepository;
    private readonly IMovieLinkRepository _movieLinkRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MovieService" /> class.
    /// </summary>
    /// <param name="movieRepository">The movie repository to use for data access.</param>
    /// <param name="movieLinkRepository">The movie link repository to use for data access.</param>
    /// <param name="logger">The logger to use for logging.</param>
    public MovieService(IMovieRepository movieRepository, IMovieLinkRepository movieLinkRepository,
        ILogger<MovieService> logger)
    {
        _movieRepository = movieRepository;
        _movieLinkRepository = movieLinkRepository;
        _logger = logger;

        _logger.LogInformation("MovieService initialized.");
    }

    /// <summary>
    ///     Gets a movie by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the movie.</param>
    /// <returns>The movie, or null if not found.</returns>
    public async Task<Movie?> GetByIdAsync(int id)
    {
        _logger.LogInformation($"Getting movie by ID: {id}");

        var movie = await _movieRepository.GetByIdAsync(id);

        if (movie == null)
        {
            _logger.LogInformation($"Movie with ID {id} not found");

            return null;
        }

        _logger.LogInformation($"Movie retrieved. Title: {movie.Title}");

        var movieLink = await _movieLinkRepository.GetById(id);
        movie!.ImdbLink = movieLink!.ImdbLink;

        return movie;
    }
}