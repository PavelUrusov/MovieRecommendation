using Microsoft.Extensions.Logging;
using Microsoft.ML;
using MovieRecommendation.TrainingModel.Common.Configs;
using MovieRecommendation.TrainingModel.Common.Helpers;
using MovieRecommendation.TrainingModel.MovieRecommendation.Services.Interfaces;

namespace MovieRecommendation.TrainingModel.MovieRecommendation.Services.Implementations;

/// <summary>
///     Service for performing grid search optimization for movie recommendation model training.
/// </summary>
internal class GridSearchOptimizerService : IGridSearchOptimizerService
{
    private readonly GridSearchConfig _config;
    private readonly DataTrainingConfig _dataTrainingConfig;
    private readonly ILogger<GridSearchOptimizerService> _logger;
    private readonly string _pathToSaveBestModelZip;
    private readonly string _pathToSaveBestResultTxt;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GridSearchOptimizerService" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="config">The grid search configuration.</param>
    /// <param name="dataTrainingConfig">The data training configuration.</param>
    /// <param name="pathToSaveBestResultTxt">The path to save the best result as a text file.</param>
    /// <param name="pathToSaveBestModelZip">The path to save the best model as a zip file.</param>
    public GridSearchOptimizerService(
        ILogger<GridSearchOptimizerService> logger,
        GridSearchConfig config,
        DataTrainingConfig dataTrainingConfig,
        string pathToSaveBestResultTxt,
        string pathToSaveBestModelZip)
    {
        _logger = logger;
        _config = config;
        _dataTrainingConfig = dataTrainingConfig;
        _pathToSaveBestResultTxt = pathToSaveBestResultTxt;
        _pathToSaveBestModelZip = pathToSaveBestModelZip;
    }

    /// <summary>
    ///     Starts the grid search optimization process.
    /// </summary>
    public void StartSearch()
    {
        _logger.LogInformation($"Grid search started");

        var bestRSquared = double.MinValue;

        foreach (var approximationRank in _config.ApproximationRanks)
        foreach (var learningRate in _config.LearningRates)
        foreach (var iterations in _config.NumberOfIterations)
        foreach (var alpha in _config.Alphas)
        foreach (var lambda in _config.Lambdas)
        {
            var matrixFactorizationConfig =
                new MatrixFactorizationConfig(lambda, approximationRank, learningRate, iterations, alpha);
            TrainAndEvaluate(matrixFactorizationConfig, ref bestRSquared);
        }
    }

    /// <summary>
    ///     Trains and evaluates a movie recommendation model using the specified configuration.
    /// </summary>
    /// <param name="matrixFactorizationConfig">The matrix factorization configuration.</param>
    /// <param name="bestRSquared">The best R-squared value obtained so far.</param>
    private void TrainAndEvaluate(MatrixFactorizationConfig matrixFactorizationConfig, ref double bestRSquared)
    {
        var trainLogger = LoggerHelper.CreateLogger<ModelTrainingService>();
        var mlContext = new MLContext();
        var trainingService =
            new ModelTrainingService(trainLogger, mlContext, _dataTrainingConfig, matrixFactorizationConfig);

        var (model, metrics) = trainingService.Fit();

        if (bestRSquared < metrics.RSquared)
        {
            bestRSquared = metrics.RSquared;

            WriteBestResult(
                metrics.RSquared,
                metrics.RootMeanSquaredError,
                matrixFactorizationConfig.NumberOfIterations,
                matrixFactorizationConfig.ApproximationRank,
                matrixFactorizationConfig.Lambda,
                matrixFactorizationConfig.Alpha,
                matrixFactorizationConfig.LearningRate);

            trainingService.SaveModel(model, _pathToSaveBestModelZip);
        }
    }

    /// <summary>
    ///     Writes the best result to a text file.
    /// </summary>
    /// <param name="rSquared">The R-squared value.</param>
    /// <param name="rms">The root mean squared error.</param>
    /// <param name="iteration">The number of iterations.</param>
    /// <param name="approximationRank">The approximation rank.</param>
    /// <param name="lambda">The lambda value.</param>
    /// <param name="alpha">The alpha value.</param>
    /// <param name="learningRate">The learning rate value.</param>
    private void WriteBestResult(
        double rSquared,
        double rms,
        int iteration,
        int approximationRank,
        float lambda,
        float alpha,
        float learningRate)
    {
        var stringWrite =
            $"1. RSquared: {rSquared}\n2. RMS: {rms}" +
            $"\n3. Iterations: {iteration}\n4. ApproximationRank: {approximationRank}" +
            $"\n5. Alpha: {alpha}\n6. Lambda: {lambda}\n7. LearningRate: {learningRate}";

        using var streamWriter = new StreamWriter(_pathToSaveBestResultTxt, false);
        streamWriter.WriteLine(stringWrite);
    }
}