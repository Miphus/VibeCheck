using Microsoft.AspNetCore.Identity;

namespace VibeCheck.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}