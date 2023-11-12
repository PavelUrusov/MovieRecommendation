using Microsoft.Extensions.Logging;
using MovieRecommendation.BLL.Entities;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.BLL.Services.Implementations;

/// <summary>
///     Represents a service for managing movie ratings.
/// </summary>
internal class MovieRatingService : IMovieRatingService
{
    private readonly ILogger<MovieRatingService> _logger;
    private readonly IMovieRatingRepository _movieRepository;
    private readonly List<BestMovie> _bestMoviesRatings;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MovieRatingService" /> class.
    /// </summary>
    /// <param name="movieRepository">The movie rating repository to use for data access.</param>
    /// <param name="logger">The logger to use for logging.</param>
    public MovieRatingService(IMovieRatingRepository movieRepository, ILogger<MovieRatingService> logger)
    {
        _movieRepository = movieRepository;
        _bestMoviesRatings = movieRepository.GetList().OrderByDescending(x => x.Label).Take(100).Select(x=> new BestMovie(x.MovieId,x.Label)).ToList();
        _logger = logger;

        _logger.LogInformation("MovieRatingService initialized.");
    }

    /// <summary>
    ///     Gets the movie rating asynchronously for the specified movie and user.
    /// </summary>
    /// <param name="movieId">The ID of the movie.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The movie rating, or null if not found.</returns>
    public async Task<MovieRating?> GetAsync(int movieId, int userId)
    {
        _logger.LogInformation($"Getting movie rating. MovieId: {movieId}, UserId: {userId}");

        var movieRating = await _movieRepository.GetAsync(movieId, userId);

        _logger.LogInformation("Movie rating retrieved.");

        return movieRating;
    }

    /// <summary>
    ///     Gets the list of all movie ratings asynchronously.
    /// </summary>
    /// <returns>The list of movie ratings.</returns>
    public async Task<List<MovieRating>> GetListAsync()
    {
        _logger.LogInformation("Getting movie ratings list.");

        var movieRatings = await _movieRepository.GetListAsync();

        _logger.LogInformation("Movie ratings list retrieved.");

        return movieRatings;
    }

    /// <summary>
    ///     Gets the list of all watched movies by the specified user asynchronously.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The list of watched movies by the user.</returns>
    public async Task<List<MovieRating>> GetAllWatchedMoviesByUserAsync(int userId)
    {
        _logger.LogInformation($"Getting all watched movies by user. UserId: {userId}");

        var movieRatings = await GetListAsync();
        var watchedMovies = movieRatings.Where(x => x.UserId == userId).ToList();

        _logger.LogInformation($"Watched movies retrieved. Count: {watchedMovies.Count}");

        return watchedMovies;
    }

    /// <summary>
    ///     Gets a list of the top 100 rated movies.
    /// </summary>
    /// <returns>List of the best films rated by users</returns>
    public List<BestMovie> GetBestMovies()
    {
        return _bestMoviesRatings;
    }
}