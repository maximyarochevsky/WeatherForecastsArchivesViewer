using Microsoft.AspNetCore.Mvc;

namespace WeatherForecastsArchivesViewer.Controllers
{
    /// <summary>
    /// �������� ����������.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// ������� ��������.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}
