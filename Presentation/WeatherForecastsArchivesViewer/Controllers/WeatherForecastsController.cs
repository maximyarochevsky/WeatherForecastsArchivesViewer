using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WeatherForecastsArchivesViewer.Application.WeatherForecast.Commands.UploadWeatherArchiveCommand;
using WeatherForecastsArchivesViewer.Application.WeatherForecast.Queries.GetAllForecastsQuery;

namespace WeatherForecastsArchivesViewer.Controllers;

/// <summary>
/// Контроллер для работы с архивами прогнозов.
/// </summary>
public class WeatherForecastsController : Controller
{
    /// <summary>
    /// Посредник для отправки запросов.
    /// </summary>
    private readonly IMediator _mediator;

    public WeatherForecastsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получение и вывод данных по прогнозам погоды.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> AllForecasts()
    {
        try
        {
            var query = new GetAllForecastsQuery();

            var result = await _mediator.Send(query);

            return View(result);
        }
        catch (Exception ex)
        {
            ViewData["ErrorMessage"] = ex.Message;
            return View("AllForecasts");
        }
    }

    /// <summary>
    /// Форма для загрузки архивов с прогнозами.
    /// </summary>
    [HttpGet]
    public IActionResult UploadForm()
    {
        return View();
    }

    /// <summary>
    /// Загрузка архивов.
    /// </summary>

    [HttpPost]
    public async Task<IActionResult> UploadForecasts(ICollection<IFormFile> files)
    {
        try
        {
            var command = new UploadWeatherArchive(files);
            var result = await _mediator.Send(command);

            ViewData["SuccessMessage"] = "Файлы успешно загружены.";
            return View("UploadForm");
        }
        catch (Exception ex)
        {
            var exceptionMessage = GetErrorMessage(ex);

            ViewData["ErrorMessage"] = exceptionMessage;
            return View("UploadForm");
        }
    }

    /// <summary>
    /// Получение сообщения об ошибке из исключения и всех его вложенных исключений.
    /// </summary>
    [NonAction]
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
