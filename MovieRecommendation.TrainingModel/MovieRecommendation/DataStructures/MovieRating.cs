using Microsoft.ML.Data;

namespace MovieRecommendation.TrainingModel.MovieRecommendation.DataStructures;

public class MovieRating
{
    [LoadColumn(0)]
    [ColumnName("UserId")]
    public int UserId { get; set; }

    [LoadColumn(1)]
    [ColumnName("MovieId")]
    public int MovieId { get; set; }

    [LoadColumn(2)]
    [ColumnName("Label")]
    public float Label { get; set; }
}