using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VibeCheck.Models;
using VibeCheck.Views.User;

namespace VibeCheck.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Lista användare
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // Detaljer för en användare
        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Lista favoriter för en användare
        public IActionResult ListFavorites(string userId)
        {
            var favorites = _context.UserFavorites
                .Where(f => f.UserId == userId)
                .Select(f => f.Artist)
                .ToList();

            return View(favorites);
        }
    }
}
