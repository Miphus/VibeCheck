using Microsoft.AspNetCore.Mvc;

namespace VibeCheck.Controlles
{
    public class BandsController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
