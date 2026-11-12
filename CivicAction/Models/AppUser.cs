using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CivicAction.Models;

public enum Grade
{
    Freshman, Sophomore, Junior, Senior
}

public class AppUser : IdentityUser
{   
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;
    [Display(Name = "First Name")]
    public string FirstMidName { get; set; } = string.Empty;

    public Grade? Grade { get; set; }
    public string? School { get; set; } = string.Empty;
    [Display(Name = "Is Admin")]    
    public bool IsAdmin { get; set; }

    public ICollection<Inclusion> Inclusions { get; set; } = new List<Inclusion>();
}