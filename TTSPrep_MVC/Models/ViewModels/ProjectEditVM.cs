
using System.ComponentModel.DataAnnotations;

namespace TTSPrep_MVC.Models.ViewModels;

public class ProjectEditVM
{
    [Required]
    public string Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}
