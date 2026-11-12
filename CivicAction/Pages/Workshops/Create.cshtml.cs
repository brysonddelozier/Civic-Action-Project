using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CivicAction.Data;
using CivicAction.Models;

namespace CivicAction.Pages.Projects;

[Authorize]
public class CreateModel(CivicActionContext context, UserManager<AppUser> userManager) : PageModel
{
    [BindProperty]
    public Project Project { get; set; } = default!;

    public AppUser? CurrentUser { get; set; }

    public async Task<IActionResult> OnGet()
    {
        CurrentUser = await userManager.GetUserAsync(User);
        LoadDropdowns();
        return Page();
    }

    private void LoadDropdowns()
    {
        var allStudents = context.AppUsers.Where(a => a.IsAdmin == false);
        ViewData["StudentId"] = new SelectList(allStudents, "Id", "FirstMidName" + " " + "LastName");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        CurrentUser = await userManager.GetUserAsync(User);
        if (CurrentUser == null) return RedirectToPage("/Account/Login");
        
        if (!ModelState.IsValid)
        {
            LoadDropdowns();
            return Page();
        }

        try
        {
            foreach (AppUser student in WorkshopStudents)
            {
                // Create a new inclusion to connect that student to this project
                Inclusion Inclusion = new Inclusion
                {
                    Student = student,
                    StudentId = student.Id,
                    FirstMidName = student.FirstMidName,
                    LastName = student.LastName,
                    Project = Project,
                    ProjectId = Project.Id,
                    Hours = WorkshopHours ?? 0
                };
                Project.Inclusions.Add(Inclusion);
                student.Inclusions.Add(Inclusion);
            }
            Project.IsWorkshop = true;
            Project.Status = Status.Approved;
            context.Projects.Add(Project);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error saving project: {ex.Message}");
            LoadDropdowns();
            return Page();
        }

        return RedirectToPage("./Index");
    }
}