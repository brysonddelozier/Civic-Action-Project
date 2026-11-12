namespace CivicAction.Models;

using System.ComponentModel.DataAnnotations;

public class Update
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Date is required")]
    public DateOnly Date { get; set; }
    
    [Required(ErrorMessage = "Hours are required")]
    public double Hours { get; set; }

    public Status Status { get; set; }

    public required int ProjectId { get; set; }
    public required Project Project { get; set; } 

    public required string StudentId { get; set; }
    public required AppUser Student { get; set; }
}