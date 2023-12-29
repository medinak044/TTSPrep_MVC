using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTSPrep_MVC.Models;

public class Chapter
{
    [Key]
    public Guid Id { get; set; }
    public string? Title { get; set; } // Default to "Chapter 1" (based on OrderNumber)
    public int OrderNumber { get; set; } // Start at 1, not 0
    [ForeignKey("Project")]
    public Guid ProjectId { get; set; }
    public ICollection<Page>? Pages { get; set; }
}
