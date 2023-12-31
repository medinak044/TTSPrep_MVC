using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTSPrep_MVC.Models;

public class Project
{
    [Key]
    public string Id { get; set; }
    public string? Title { get; set; } // Default to "project_123" (based on project id)
    public string? Description { get; set; }
    public DateTime? CreatedDate { get; set; } // = DateTime.Now;
    public DateTime LastModifiedDate { get; set; }
    [ForeignKey("AppUser")]
    public string? OwnerId { get; set; }
    public AppUser? Owner { get; set; }
    public ICollection<Chapter>? Chapters { get; set; }
    public ICollection<Word>? Words { get; set; }
    [ForeignKey("Chapter")]
    public string? CurrentChapterId { get; set; }
    public Chapter? Chapter { get; set; }
}
