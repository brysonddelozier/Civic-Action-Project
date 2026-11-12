using System.ComponentModel.DataAnnotations;

namespace CivicAction.Models;

public class Inclusion
{
    public int Id { get; set; }

    public required string StudentId { get; set; }
    public required AppUser Student { get; set; }
    public string? FirstMidName { get; set; }
    public string? LastName { get; set; }

    public required int ProjectId { get; set; }
    public required Project Project { get; set; }

    public double Hours { get; set; }
}