using CsvHelper.Configuration.Attributes;
using MovieRecommendation.BLL.Common.Helpers;

namespace MovieRecommendation.BLL.Entities;

public class Movie
{
    [Name("title")] public string Title { get; set; } = null!;

    [Name("genres")]
    [TypeConverter(typeof(StringMovieGenresConverter))]
    public string[] Genres { get; set; } = null!;

    [Name("movieId")] public int Id { get; set; }

    [Ignore] public string? ImdbLink { get; set; }
}