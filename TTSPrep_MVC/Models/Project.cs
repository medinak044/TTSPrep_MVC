using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTSPrep_MVC.Models;

public class Project
{
    [Key]
    public Guid Id { get; set; }
    public string? Title { get; set; } // Default to "untitled-123" (based on id)
    public string? Description { get; set; }
    public DateTime? CreatedDate { get; set; } // = DateTime.Now;
    public DateTime LastModifiedDate { get; set; }
    [ForeignKey("AppUser")]
    public string? OwnerId { get; set; }
    public AppUser? Owner { get; set; }
    public ICollection<Chapter>? Chapters { get; set; }
}
