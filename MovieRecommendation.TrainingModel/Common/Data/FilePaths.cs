namespace MovieRecommendation.TrainingModel.Common.Data;

public static class FilePaths
{
    public static string CurrentDirectory { get; } = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));
    public static string DataDirectory { get; set; } = Path.GetFullPath(Path.Combine(CurrentDirectory, "..", "Data"));

    public static string MovieRating { get; set; } =
        Path.GetFullPath(Path.Combine(DataDirectory, "movie_lens_ratings_25ml_filtered.csv"));

    public static string Movies { get; set; } =
        Path.GetFullPath(Path.Combine(DataDirectory, "movie_lens_movies_25ml_filtered.csv"));

    public static string MovieLinks { get; set; } =
        Path.GetFullPath(Path.Combine(DataDirectory, "movie_lens_links_25ml_filtered.csv"));

    public static string Model { get; set; } =
        Path.GetFullPath(Path.Combine(CurrentDirectory, "..", "Model", "model.zip"));

    public static string LastModelHyperparameters =
        Path.GetFullPath(Path.Combine(CurrentDirectory, "..", "Model", "last_best_model_params.txt"));
    public static string LastModel { get; set; } =
        Path.GetFullPath(Path.Combine(CurrentDirectory, "..", "Model", "last_best_model.zip"));
}