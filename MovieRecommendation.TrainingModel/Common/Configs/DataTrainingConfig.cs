namespace MovieRecommendation.TrainingModel.Common.Configs;

public class DataTrainingConfig
{
    public readonly bool HasHeader;

    public readonly string Path;
    public readonly char Separator;

    public DataTrainingConfig(string path, char separator = '\t', bool hasHeader = false)
    {
        Path = path;
        Separator = separator;
        HasHeader = hasHeader;
    }
}