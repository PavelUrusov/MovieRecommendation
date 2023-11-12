using Microsoft.ML.Data;
using Microsoft.ML;
using MovieRecommendation.TrainingModel.MovieRecommendation.DataStructures;

namespace MovieRecommendation.TrainingModel.MovieRecommendation.Services.Interfaces;

public interface IModelTrainingService
{
    (ITransformer model, RegressionMetrics metrics) Fit();
    void SaveModel(ITransformer model, string path);
    MovieRatingPrediction Predict(ITransformer model, MovieRating inputPrediction);
    ITransformer LoadModel(string path);
}