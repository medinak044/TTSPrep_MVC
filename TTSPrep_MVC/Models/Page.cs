using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTSPrep_MVC.Models;

public class Page
{
    [Key]
    public Guid Id { get; set; }
    public int OrderNumber { get; set; }
    public string? Text { get; set; }
    [ForeignKey("Chapter")]
    public Guid ChapterId { get; set; }
}
