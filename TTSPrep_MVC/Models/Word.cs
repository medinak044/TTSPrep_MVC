using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTSPrep_MVC.Models;

public class Word
{
    [Key]
    public Guid Id { get; set; }
    public string OriginalSpelling { get; set; }
    public string? ModifiedSpelling { get; set; }
    [ForeignKey("Project")]
    public Guid ProjectId { get; set; }
}
