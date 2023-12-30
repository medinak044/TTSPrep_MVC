using System.ComponentModel.DataAnnotations;

namespace TTSPrep_MVC.Models.ViewModels;

public class ProjectVM
{
    [Display(Name = "Title")]
    public string? Title { get; set; }    
    [Display(Name = "Description")]
    public string? Description { get; set; }
    public string OwnerId { get; set; }
}
