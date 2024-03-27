using JetBrains.Annotations;

namespace WeatherForecastsArchivesViewer.Application.Interfaces.Persistence;

/// <summary>
/// Провайдер БД транзакций.
/// </summary>
[PublicAPI]
public interface IDatabasePort
{
    /// <summary>
    /// Выполнить операцию в транзакции.
    /// </summary>
    T Execute<T>([InstantHandle] Func<IUnitOfWork, T> func);

    /// <summary>
    /// Выполнить операцию в транзакции асинхронно.
    /// </summary>
    Task<T> Execute<T>([InstantHandle] Func<IUnitOfWork, Task<T>> func);

    /// <summary>
    /// Выполнить операцию в транзакции.
    /// </summary>
    public void Execute([InstantHandle] Action<IUnitOfWork> func) => Execute(
        unitOfWork =>
        {
            func(unitOfWork);
            return (object?)null;
        }
    );
}
