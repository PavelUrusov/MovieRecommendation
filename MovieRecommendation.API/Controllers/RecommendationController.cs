using Microsoft.AspNetCore.Mvc;
using MovieRecommendation.BLL.Common.Dtos;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.API.Controllers;

/// <summary>
///     Controller for managing movie recommendations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly ILogger<RecommendationController> _logger;
    private readonly IMovieRatingService _movieRatingService;
    private readonly IRecommendationService _recommendationService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RecommendationController" /> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="recommendationService">The recommendation service.</param>
    public RecommendationController(ILogger<RecommendationController> logger,
        IRecommendationService recommendationService, IMovieRatingService movieRatingService)
    {
        _logger = logger;
        _recommendationService = recommendationService;
        _movieRatingService = movieRatingService;
    }

    /// <summary>
    ///     Retrieves movie recommendations for a user based on the provided input.
    /// </summary>
    /// <param name="dto">The input DTO containing user details.</param>
    /// <returns>The IActionResult containing the recommended movies.</returns>
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetRecommendationForUser([FromQuery] GetRecommendationForUserDto dto)
    {
        _logger.LogInformation("Received request to get movie recommendations for a user.");

        var result = await _recommendationService.GetRecommendMoviesAsync(dto);

        _logger.LogInformation("Movie recommendations retrieved successfully.");

        return StatusCode(200, result);
    }

    /// <summary>
    ///     Performs prediction assessment.
    /// </summary>
    /// <returns>The IActionResult containing the result of the prediction assessment.</returns>
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> PredictionAssessment()
    {
        _logger.LogInformation("Performing prediction assessment.");

        var result = await _recommendationService.PredictionAssessment();

        _logger.LogInformation("Prediction assessment completed successfully.");

        return StatusCode(200, result);
    }

    /// <summary>
    /// Retrieves the best movies.
    /// </summary>
    /// <returns>The IActionResult containing the best movies.</returns>
    [HttpGet]
    [Route("[action]")]
    public IActionResult BestMovies()
    {
        _logger.LogInformation("Retrieving the best movies.");

        var result = _movieRatingService.GetBestMovies();

        _logger.LogInformation("Best movies retrieved successfully.");

        return StatusCode(200, result);
    }
}