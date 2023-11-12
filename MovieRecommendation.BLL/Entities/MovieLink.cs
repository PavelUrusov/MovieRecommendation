using CsvHelper.Configuration.Attributes;

namespace MovieRecommendation.BLL.Entities;

public class MovieLink
{
    [Name("movieId")] public int MovieId { get; set; }

    [Name("imdbId")] public string ImdbId { get; set; } = null!;

    [Ignore] public string? ImdbLink { get; set; }
}