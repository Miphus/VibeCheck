using Microsoft.AspNetCore.Mvc;

namespace VibeCheck.Controlles
{
    public class ArtistsController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/add")]
        public IActionResult Add()
        {
            return View();
        }

    }
}
