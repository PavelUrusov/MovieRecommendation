using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using MovieRecommendation.TrainingModel.Common.Configs;
using MovieRecommendation.TrainingModel.MovieRecommendation.DataStructures;
using MovieRecommendation.TrainingModel.MovieRecommendation.Services.Interfaces;

namespace MovieRecommendation.TrainingModel.MovieRecommendation.Services.Implementations;

/// <summary>
///     Service for training and evaluating a movie recommendation model.
/// </summary>
internal class ModelTrainingService : IModelTrainingService
{
    private readonly DataTrainingConfig _dataTrainingConfig;
    private readonly ILogger<ModelTrainingService> _logger;
    private readonly MatrixFactorizationConfig _matrixFactorizationConfig;
    private readonly MLContext _mlContext;
    public readonly IDataView TestSet;
    public readonly IDataView TrainSet;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ModelTrainingService" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="mlContext">The MLContext.</param>
    /// <param name="dataTrainingConfig">The data training configuration.</param>
    /// <param name="matrixFactorizationConfig">The matrix factorization configuration.</param>
    public ModelTrainingService(ILogger<ModelTrainingService> logger, MLContext mlContext,
        DataTrainingConfig dataTrainingConfig, MatrixFactorizationConfig matrixFactorizationConfig)
    {
        _logger = logger;
        _mlContext = mlContext;
        _dataTrainingConfig = dataTrainingConfig;
        _matrixFactorizationConfig = matrixFactorizationConfig;
        (TrainSet, TestSet) = LoadAndSplitDataFromTextFile(_dataTrainingConfig.Path, _dataTrainingConfig.Separator,
            _dataTrainingConfig.HasHeader);
    }

    /// <summary>
    ///     Fits the model by training and evaluating it.
    /// </summary>
    /// <returns>The trained model and evaluation metrics.</returns>
    public (ITransformer model, RegressionMetrics metrics) Fit()
    {
        _logger.LogInformation("=============== Training the model ===============");
        var pipeline = BuildPipeline();
        var model = pipeline.Fit(TrainSet);

        _logger.LogInformation("=============== Evaluating the model ===============");
        var metrics = EvaluateModel(model);

        return (model, metrics);
    }

    /// <summary>
    ///     Saves the trained model to a file.
    /// </summary>
    /// <param name="model">The trained model.</param>
    /// <param name="path">The file path.</param>
    public void SaveModel(ITransformer model, string path)
    {
        _logger.LogInformation("Saving the model...");

        _mlContext.Model.Save(model, TrainSet.Schema, path);
    }

    /// <summary>
    ///     Predicts the movie rating for a given input.
    /// </summary>
    /// <param name="model">The trained model.</param>
    /// <param name="inputPrediction">The input prediction.</param>
    /// <returns>The predicted movie rating.</returns>
    public MovieRatingPrediction Predict(ITransformer model, MovieRating inputPrediction)
    {
        var predictionEngine = _mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
        var movieRatingPrediction = predictionEngine.Predict(inputPrediction);

        return movieRatingPrediction;
    }

    /// <summary>
    ///     Loads a trained model from a file.
    /// </summary>
    /// <param name="path">The file path.</param>
    /// <returns>The loaded model.</returns>
    public ITransformer LoadModel(string path)
    {
        _logger.LogInformation("Loading the model...");

        var loadedModel = _mlContext.Model.Load(path, out var schema);

        return loadedModel;
    }

    /// <summary>
    ///     Evaluates the model using the test data.
    /// </summary>
    /// <param name="model">The trained model.</param>
    /// <returns>The evaluation metrics.</returns>
    protected RegressionMetrics EvaluateModel(ITransformer model)
    {
        _logger.LogInformation("Evaluating the model");
        var prediction = model.Transform(TestSet);
        var metrics = _mlContext.Regression.Evaluate(prediction);
        _logger.LogInformation(
            $"The model evaluation metrics RootMeanSquaredError: {metrics.RootMeanSquaredError}  and RSquared: {metrics.RSquared}");

        return metrics;
    }

    /// <summary>
    ///     Builds the pipeline for training the model.
    /// </summary>
    /// <returns>The pipeline.</returns>
    protected IEstimator<ITransformer> BuildPipeline()
    {
        _logger.LogInformation("Building the pipeline");

        var options = BuildMatrixFactorizationOptions();
        var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("movieIdEncoded", nameof(MovieRating.MovieId))
            .Append(_mlContext.Transforms.Conversion.MapValueToKey("userIdEncoded", nameof(MovieRating.UserId)))
            .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(options));
        return pipeline;
    }

    /// <summary>
    ///     Builds the matrix factorization trainer options.
    /// </summary>
    /// <returns>The matrix factorization options.</returns>
    protected MatrixFactorizationTrainer.Options BuildMatrixFactorizationOptions()
    {
        return new MatrixFactorizationTrainer.Options
        {
            ApproximationRank = _matrixFactorizationConfig.ApproximationRank,
            Alpha = _matrixFactorizationConfig.Alpha,
            NumberOfIterations = _matrixFactorizationConfig.NumberOfIterations,
            LearningRate = _matrixFactorizationConfig.LearningRate,
            Lambda = _matrixFactorizationConfig.Lambda,
            LabelColumnName = "Label",
            MatrixRowIndexColumnName = "userIdEncoded",
            MatrixColumnIndexColumnName = "movieIdEncoded"
        };
    }

    /// <summary>
    ///     Loads and splits the data from a text file.
    /// </summary>
    /// <param name="path">The file path.</param>
    /// <param name="separator">The separator character.</param>
    /// <param name="hasHeader">Specifies if the file has a header.</param>
    /// <returns>The train and test data views.</returns>
    protected (IDataView TrainSet, IDataView TestSet) LoadAndSplitDataFromTextFile(string path, char separator = '\t',
        bool hasHeader = false)
    {
        _logger.LogInformation("Loading and splitting the data from a text file");

        var data = _mlContext.Data.LoadFromTextFile<MovieRating>(path, separator, hasHeader);
        var trainTestData = _mlContext.Data.TrainTestSplit(data);

        return (trainTestData.TrainSet, trainTestData.TestSet);
    }
}