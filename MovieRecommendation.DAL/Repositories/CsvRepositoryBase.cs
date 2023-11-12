using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MovieRecommendation.BLL.Services.Interfaces;

namespace MovieRecommendation.DAL.Repositories;

/// <summary>
///     Abstract base class for CSV repositories.
/// </summary>
/// <typeparam name="T">The type of entities stored in the repository.</typeparam>
internal abstract class CsvRepositoryBase<T> : IDataRepository<T>
    where T : new()
{
    private readonly CsvConfiguration _csvConfiguration;
    private readonly string _filePath;
    protected readonly Lazy<Task<List<T>>> Repository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CsvRepositoryBase{T}" /> class with the specified file path.
    /// </summary>
    /// <param name="filePath">The path to the CSV file.</param>
    protected CsvRepositoryBase(string filePath)
    {
        _filePath = filePath;
        _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ","
        };
        Repository = new Lazy<Task<List<T>>>(LoadCsvFileAsync);
    }

    /// <summary>
    ///     Gets the list of entities from the repository.
    /// </summary>
    /// <returns>The list of entities.</returns>
    public List<T> GetList()
    {
        var repository = Repository.Value.Result;

        return repository;
    }

    /// <summary>
    ///     Gets the list of entities from the repository asynchronously.
    /// </summary>
    /// <returns>The list of entities.</returns>
    public async Task<List<T>> GetListAsync()
    {
        var repository = await Repository.Value;

        return repository;
    }

    /// <summary>
    ///     Loads the CSV file and returns a list of entities.
    /// </summary>
    /// <returns>The list of entities.</returns>
    protected async Task<List<T>> LoadCsvFileAsync()
    {
        using var reader = new StreamReader(_filePath);
        using var csv = new CsvReader(reader, _csvConfiguration);
        var records = new List<T>();

        await foreach (var record in csv.GetRecordsAsync<T>()) records.Add(record);

        return records;
    }
}