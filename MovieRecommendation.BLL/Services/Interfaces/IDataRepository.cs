namespace MovieRecommendation.BLL.Services.Interfaces;

/// <summary>
///     Represents a generic data repository interface.
/// </summary>
/// <typeparam name="T">The type of data to be managed by the repository.</typeparam>
public interface IDataRepository<T>
{
    /// <summary>
    ///     Gets a list of data items synchronously.
    /// </summary>
    /// <returns>The list of data items.</returns>
    List<T> GetList();

    /// <summary>
    ///     Gets a list of data items asynchronously.
    /// </summary>
    /// <returns>The list of data items.</returns>
    Task<List<T>> GetListAsync();
}