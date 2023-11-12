using Microsoft.Extensions.Logging;
using MovieRecommendation.BLL.Common.Dtos;
using MovieRecommendation.BLL.Entities;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.BLL.Services.Implementations;

/// <summary>
///     Represents a service for movie recommendations.
/// </summary>
internal class RecommendationService : IRecommendationService
{
    private readonly ILogger<RecommendationService> _logger;
    private readonly IModelService _modelService;
    private readonly IMovieRatingService _movieRatingService;
    private readonly IMovieRepository _movieRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RecommendationService" /> class.
    /// </summary>
    /// <param name="movieRatingService">The movie rating service to use for movie ratings.</param>
    /// <param name="modelService">The model service to use for predictions.</param>
    /// <param name="logger">The logger to use for logging.</param>
    /// <param name="movieRepository">The movie repository to use for movie data access.</param>
    public RecommendationService(IMovieRatingService movieRatingService, IModelService modelService,
        ILogger<RecommendationService> logger, IMovieRepository movieRepository)
    {
        _movieRatingService = movieRatingService;
        _modelService = modelService;
        _logger = logger;
        _movieRepository = movieRepository;

        _logger.LogInformation("RecommendationService initialized.");
    }

    /// <summary>
    ///     Retrieves a list of recommended movies for a given user.
    /// </summary>
    /// <param name="dto">The DTO (Data Transfer Object) containing the user ID and count of recommended movies.</param>
    /// <returns>A list of MovieRatingPrediction representing the recommended movies.</returns>
    public async Task<List<MovieRatingPrediction>> GetRecommendMoviesAsync(GetRecommendationForUserDto dto)
    {
        _logger.LogInformation($"Getting recommended movies. UserId: {dto.UserId}, Count: {dto.Count}");

        var predictionEngine = _modelService.CreatePredictionEngine<MovieRating, MovieRatingPrediction>();
        var watchedMovies = await _movieRatingService.GetAllWatchedMoviesByUserAsync(dto.UserId);
        var movies = await _movieRepository.GetListAsync();

        _logger.LogInformation($"Total movies: {movies.Count}");

        var predictions = movies
            .Select(x =>
            {
                var rating = new MovieRating { MovieId = x.Id, UserId = dto.UserId };
                return _modelService.Predict(rating, predictionEngine);
            })
            .Where(x => !watchedMovies.Select(movieRating => movieRating.MovieId).Contains(x.MovieId))
            .OrderByDescending(x => x.Score)
            .Take(dto.Count)
            .ToList();

        _logger.LogInformation($"Recommended movies retrieved. Count: {predictions.Count}");

        return predictions;
    }

    /// <summary>
    ///     Performs prediction assessment.
    /// </summary>
    /// <returns>A MovieRatingPrediction representing the prediction assessment result.</returns>
    public async Task<MovieRatingPrediction> PredictionAssessment()
    {
        _logger.LogInformation("Performing prediction assessment.");

        var predictionEngine = _modelService.CreatePredictionEngine<MovieRating, MovieRatingPrediction>();
        var randomizer = new Random();

        var movieRatings = await _movieRatingService.GetListAsync();
        var movieRating = movieRatings[randomizer.Next(0, 20000)];

        var prediction = _modelService.Predict(movieRating, predictionEngine);

        _logger.LogInformation("Prediction assessment completed successfully.");

        return prediction;
    }

}