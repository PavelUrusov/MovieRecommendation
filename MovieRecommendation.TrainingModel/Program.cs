using MovieRecommendation.TrainingModel.Common.Configs;
using MovieRecommendation.TrainingModel.Common.Data;
using MovieRecommendation.TrainingModel.Common.Helpers;
using MovieRecommendation.TrainingModel.MovieRecommendation.Services.Implementations;

namespace MovieRecommendation.TrainingModel;

internal class Program
{
    private static void Main(string[] args)
    {
        var logger = LoggerHelper.CreateLogger<GridSearchOptimizerService>();
        var gridSearchOptions = new GridSearchConfig
        {
            Alphas = new[] { 0.01f,0.01f,0,03f },
            NumberOfIterations = new[] { 20,50,100,200,300 },
            LearningRates = new[] { 0.001f,0.2f,0.05f,0.07f,0.1f, },
            ApproximationRanks = new[] { 100,200,300,400,500,800 },
            Lambdas = new[] { 0,01f,0.05f,0,08f,0.1f,0.2f}
        };
        var dataTrainConfig = new DataTrainingConfig(FilePaths.MovieRating, ',', true);

        var gridSearchService = new GridSearchOptimizerService(logger, gridSearchOptions,dataTrainConfig,FilePaths.LastModelHyperparameters,FilePaths.LastModel);
        gridSearchService.StartSearch();
    }
}