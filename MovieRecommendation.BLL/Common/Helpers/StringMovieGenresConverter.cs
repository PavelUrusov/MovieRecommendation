using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace MovieRecommendation.BLL.Common.Helpers;

/// <summary>
///     Custom type converter for converting string movie genres from CSV.
/// </summary>
internal class StringMovieGenresConverter : DefaultTypeConverter
{
    /// <summary>
    ///     Converts a string representation of movie genres into an array of genre strings.
    /// </summary>
    /// <param name="text">The string representation of movie genres.</param>
    /// <param name="row">The current reader row.</param>
    /// <param name="memberMapData">The member mapping data.</param>
    /// <returns>An array of genre strings.</returns>
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        return string.IsNullOrEmpty(text) ? Array.Empty<string>() : text.Split('|').Select(s => s.Trim()).ToArray();
    }
}