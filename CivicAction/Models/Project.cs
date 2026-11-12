using System.ComponentModel.DataAnnotations;

namespace CivicAction.Models;

public enum Status 
{
    Approved, Pending, Rejected
}

public class Project
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Description is required")]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; } = string.Empty;

    public string? Organization { get; set; } = string.Empty;

    [Display(Name = "Site Location")]
    public string SiteLocation { get; set; } = string.Empty;
    
    [Display (Name = "Start Date")]
    public DateOnly Start { get; set; }

    [Display (Name = "End Date")]
    public DateOnly End { get; set; }

    public Status? Status { get; set; }

    [Display(Name = "Workshop")]
    public bool IsWorkshop { get; set; }
    [Display(Name = "Archived")]
    public bool IsArchived { get; set; }

    public ICollection<Inclusion> Inclusions { get; set; } = new List<Inclusion>();
}