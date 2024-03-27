using Microsoft.AspNetCore.Mvc;

namespace WeatherForecastsArchivesViewer.Controllers
{
    /// <summary>
    /// Основной контроллер.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Главная страница.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}
