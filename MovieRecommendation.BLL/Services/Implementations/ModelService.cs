using Microsoft.Extensions.Logging;
using Microsoft.ML;
using MovieRecommendation.BLL.Common.Data;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.BLL.Services.Implementations;

/// <summary>
///     Represents a service for working with ML models.
/// </summary>
internal class ModelService : IModelService
{
    private readonly DataViewSchema _dataViewSchema;
    private readonly ILogger<ModelService> _logger;
    private readonly MLContext _mlContext;
    private readonly ITransformer _model;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ModelService" /> class.
    /// </summary>
    /// <param name="logger">The logger to use for logging.</param>
    /// <param name="mlContext">The MLContext instance to use for ML operations.</param>
    public ModelService(ILogger<ModelService> logger, MLContext mlContext)
    {
        _logger = logger;
        _mlContext = mlContext;
        (_model, _dataViewSchema) = LoadModel();

        _logger.LogInformation("ModelService initialized.");
    }

    /// <summary>
    ///     Performs prediction using the specified input and prediction engine.
    /// </summary>
    /// <typeparam name="TInputModel">The type of the input model.</typeparam>
    /// <typeparam name="TOutputModel">The type of the output model.</typeparam>
    /// <param name="input">The input model for prediction.</param>
    /// <param name="predictionEngine">The prediction engine to use for prediction.</param>
    /// <returns>The predicted output model.</returns>
    public TOutputModel Predict<TInputModel, TOutputModel>(TInputModel input,
        PredictionEngine<TInputModel, TOutputModel> predictionEngine)
        where TOutputModel : class, new()
        where TInputModel : class
    {
        _logger.LogInformation("Performing prediction.");

        var predict = predictionEngine.Predict(input);

        _logger.LogInformation("Prediction completed.");

        return predict;
    }

    /// <summary>
    ///     Creates a prediction engine for the specified input and output types.
    /// </summary>
    /// <typeparam name="TInputModel">The type of the input model.</typeparam>
    /// <typeparam name="TOutputModel">The type of the output model.</typeparam>
    /// <returns>The created prediction engine.</returns>
    public PredictionEngine<TInputModel, TOutputModel> CreatePredictionEngine<TInputModel, TOutputModel>()
        where TInputModel : class
        where TOutputModel : class, new()
    {
        _logger.LogInformation("Prediction engine created.");

        var predictionEngine =
            _mlContext.Model.CreatePredictionEngine<TInputModel, TOutputModel>(_model, _dataViewSchema);

        return predictionEngine;
    }

    /// <summary>
    ///     Loads the ML model and returns the loaded model and data view schema.
    /// </summary>
    /// <returns>The loaded model and data view schema.</returns>
    protected (ITransformer model, DataViewSchema schema) LoadModel()
    {
        _logger.LogInformation("Loading model.");

        var model = _mlContext.Model.Load(FilePaths.Model, out var schema);

        _logger.LogInformation("Model loaded.");

        return (model, schema);
    }
}