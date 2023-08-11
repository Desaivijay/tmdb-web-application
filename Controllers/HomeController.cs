using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using tmdb_web_application.Data;
using tmdb_web_application.Models;

namespace tmdb_web_application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TmdbService _tmdbService;

        public HomeController(ILogger<HomeController> logger, TmdbService tmdbService)
        {
            _logger = logger;
            _tmdbService = tmdbService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _tmdbService.GetPopularMoviesAsync();

            // Convert the Movie objects to MovieViewModel objects
            var movieViewModels = movies.Select(m => new MovieViewModel { Title = m.Title });

            return View(movieViewModels.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
