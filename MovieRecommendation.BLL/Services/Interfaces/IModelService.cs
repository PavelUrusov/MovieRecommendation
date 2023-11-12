using Microsoft.ML;

namespace MovieRecommendation.BLL.Services.Interfaces;

/// <summary>
///     Represents a service for working with ML models.
/// </summary>
public interface IModelService
{
    /// <summary>
    ///     Performs a prediction using the provided input and prediction engine.
    /// </summary>
    /// <typeparam name="TInputModel">The type of the input model.</typeparam>
    /// <typeparam name="TOutputModel">The type of the output model.</typeparam>
    /// <param name="input">The input model for the prediction.</param>
    /// <param name="predictionEngine">The prediction engine to use.</param>
    /// <returns>The predicted output model.</returns>
    TOutputModel Predict<TInputModel, TOutputModel>(TInputModel input,
        PredictionEngine<TInputModel, TOutputModel> predictionEngine)
        where TOutputModel : class, new()
        where TInputModel : class;

    /// <summary>
    ///     Creates a prediction engine for the specified input and output models.
    /// </summary>
    /// <typeparam name="TInputModel">The type of the input model.</typeparam>
    /// <typeparam name="TOutputModel">The type of the output model.</typeparam>
    /// <returns>The created prediction engine.</returns>
    PredictionEngine<TInputModel, TOutputModel> CreatePredictionEngine<TInputModel, TOutputModel>()
        where TInputModel : class
        where TOutputModel : class, new();
}