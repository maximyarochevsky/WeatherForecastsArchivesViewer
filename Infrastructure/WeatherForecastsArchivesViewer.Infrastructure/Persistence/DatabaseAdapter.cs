using System.Text;
using Microsoft.Extensions.Logging;
using Npgsql;
using PigTrade.DataPlant.Abstractions.Ports.Database;
using WeatherForecastsArchiveViewer.Application.Interfaces.Persistence;

namespace WeatherForecastsArchiveViewer.Infrastructure.Persistence;

/// <inheritdoc cref="IDatabasePort"/>
internal class DatabaseAdapter : IDatabasePort
{
    /// <summary>
    /// �������� ��.
    /// </summary>
    private readonly DatabaseContext _db;

    /// <summary>
    /// ��������� ������������.
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// ������ ��� ������� ������� ������ ���������.
    /// </summary>
    private readonly ILogger<DatabaseAdapter> _logger;

    /// <inheritdoc cref="DatabaseAdapter"/>
    public DatabaseAdapter(DatabaseContext db, IUnitOfWork unitOfWork, ILogger<DatabaseAdapter> logger)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc cref="IDatabasePort.Execute{T}(Func{IUnitOfWork, Task{T}})"/>
    public async Task<T> Execute<T>(Func<IUnitOfWork, Task<T>> func)
    {
        const int maxAttempts = 5;

        for (var i = 0; ; i++)
        {
            try
            {
                using var transaction = _db.Database.BeginTransaction();
                var result = await func(_unitOfWork);
                transaction.Commit();
                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e) when (i < maxAttempts && IsTransientException(e))
            {
                var errorMessage = GetErrorMessage(e);
                _logger.LogWarning("A transient exception occured, will now retry. {Message}", errorMessage);
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
        }
    }

    /// <inheritdoc cref="IDatabasePort.Execute{T}(Func{IUnitOfWork, T})"/>
    public T Execute<T>(Func<IUnitOfWork, T> func)
    {
        const int maxAttempts = 5;

        for (var i = 0; ; i++)
        {
            try
            {
                using var transaction = _db.Database.BeginTransaction();
                var result = func(_unitOfWork);
                transaction.Commit();
                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e) when (i < maxAttempts && IsTransientException(e))
            {
                var errorMessage = GetErrorMessage(e);
                _logger.LogWarning("A transient exception occured, will now retry. {Message}", errorMessage);
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
        }
    }

    /// <summary>
    /// ���������, �������� �� ���������� ������������. 
    /// </summary>
    private static bool IsTransientException(Exception? e)
    {
        if (e == null)
        {
            return false;
        }

        if (e is NpgsqlException ex)
        {
            return ex.IsTransient;
        }

        return IsTransientException(e.InnerException);
    }

    /// <summary>
    /// ��������� ��������� �� ������ �� ���������� � ���� ��� ��������� ����������.
    /// </summary>
    private static string GetErrorMessage(Exception e)
    {
        var sb = new StringBuilder();

        var ex = e;
        while (ex != null)
        {
            if (sb.Length > 0)
            {
                sb.Append('\n');
            }

            sb.Append(ex.Message);
            ex = ex.InnerException;
        }

        return sb.ToString();
    }
}
