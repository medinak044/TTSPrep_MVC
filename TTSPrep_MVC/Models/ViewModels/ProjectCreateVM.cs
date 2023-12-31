using System.ComponentModel.DataAnnotations;

namespace TTSPrep_MVC.Models.ViewModels;

public class ProjectCreateVM
{
    [Display(Name = "Title")]
    public string? Title { get; set; }    
    [Display(Name = "Description")]
    public string? Description { get; set; }
    public string? OwnerId { get; set; } // Property made nullable to pass ModelState validation
}
