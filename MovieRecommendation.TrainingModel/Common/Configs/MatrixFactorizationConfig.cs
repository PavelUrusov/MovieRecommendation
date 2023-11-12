namespace MovieRecommendation.TrainingModel.Common.Configs;

public class MatrixFactorizationConfig
{
    public readonly float Alpha;
    public readonly int ApproximationRank;
    public readonly float Lambda;
    public readonly float LearningRate;
    public readonly int NumberOfIterations;

    public MatrixFactorizationConfig(float lambda, int approximationRank, float learningRate, int numberOfIterations,
        float alpha)
    {
        Lambda = lambda;
        ApproximationRank = approximationRank;
        LearningRate = learningRate;
        NumberOfIterations = numberOfIterations;
        Alpha = alpha;
    }
}