using CsvHelper.Configuration.Attributes;

namespace MovieRecommendation.BLL.Entities;

public class MovieRating
{
    [Name("userId")] public int UserId { get; set; }

    [Name("movieId")] public int MovieId { get; set; }

    [Name("rating")] public float Label { get; set; }
}