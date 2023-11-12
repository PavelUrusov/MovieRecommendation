namespace MovieRecommendation.TrainingModel.Common.Configs;

public class GridSearchConfig
{
    public int[] ApproximationRanks { get; set; } = null!;
    public float[] LearningRates { get; set; } = null!;
    public int[] NumberOfIterations { get; set; } = null!;
    public float[] Alphas { get; set; } = null!;
    public float[] Lambdas { get; set; } = null!;
}