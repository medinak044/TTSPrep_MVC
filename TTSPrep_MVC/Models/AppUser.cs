using Microsoft.AspNetCore.Identity;

namespace TTSPrep_MVC.Models;

public class AppUser: IdentityUser
{
    // Use email as username
    public DateTime DateCreated { get; set; }
    public ICollection<Project>? Projects { get; set; }
}
