using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace VibeCheck.Models
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) :
    IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
    {
    }
}