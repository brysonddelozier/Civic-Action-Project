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

    public int? ProjectId { get; set; }
    public Project? Project { get; set; } 

    public string? StudentId { get; set; }
    public AppUser? Student { get; set; }
}