using MovieRecommendation.BLL.Common.Dtos;
using MovieRecommendation.BLL.Entities;

namespace MovieRecommendation.BLL.Services.Interfaces;

/// <summary>
///     Represents a service for movie recommendations.
/// </summary>
public interface IRecommendationService
{
    /// <summary>
    ///     Retrieves a list of recommended movies for a given user.
    /// </summary>
    /// <param name="dto">The DTO (Data Transfer Object) containing the user ID and count of recommended movies.</param>
    /// <returns>A list of MovieRatingPrediction representing the recommended movies.</returns>
    Task<List<MovieRatingPrediction>> GetRecommendMoviesAsync(GetRecommendationForUserDto dto);

    /// <summary>
    ///     Performs prediction assessment.
    /// </summary>
    /// <returns>A MovieRatingPrediction representing the prediction assessment result.</returns>
    Task<MovieRatingPrediction> PredictionAssessment();
}