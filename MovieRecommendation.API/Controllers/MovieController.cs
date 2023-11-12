using Microsoft.AspNetCore.Mvc;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.API.Controllers;

/// <summary>
///     Represents a controller for managing movies.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    private readonly IMovieService _movieService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MovieController" /> class.
    /// </summary>
    /// <param name="logger">The logger to use for logging.</param>
    /// <param name="movieService">The movie service to use for movie operations.</param>
    public MovieController(ILogger<MovieController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }

    /// <summary>
    ///     Gets a movie by its ID.
    /// </summary>
    /// <param name="movieId">The ID of the movie to retrieve.</param>
    /// <returns>An IActionResult representing the operation result.</returns>
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Get([FromQuery] int movieId)
    {
        _logger.LogInformation($"Getting movie by ID: {movieId}");

        var result = await _movieService.GetByIdAsync(movieId);

        if (result == null)
        {
            _logger.LogInformation($"Movie with ID: {movieId} not found");
            return StatusCode(400);
        }

        _logger.LogInformation($"Movie with ID: {movieId} retrieved successfully");

        return StatusCode(200, result);
    }
}