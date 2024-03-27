using JetBrains.Annotations;
using WeatherForecastsArchivesViewer.Application.Interfaces.Persistence;

namespace PigTrade.DataPlant.Abstractions.Ports.Database;

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
