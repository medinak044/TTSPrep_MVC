using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTSPrep_MVC.Models;

public class Chapter
{
    [Key]
    public string Id { get; set; }
    public string? Title { get; set; } // Default to "Chapter 1" (based on OrderNumber)
    public int OrderNumber { get; set; } // Start at 1, not 0
    public string? OriginalText { get; set; }
    public string? ModifiedText { get; set; }
    [ForeignKey("Project")]
    public string ProjectId { get; set; }
    //public ICollection<Page>? Pages { get; set; }
}
