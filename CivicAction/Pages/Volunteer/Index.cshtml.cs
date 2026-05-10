using CivicAction.Data;
using CivicAction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CivicAction.Pages.Volunteer;

[Authorize]
public class IndexModel(CivicActionContext context, UserManager<AppUser> userManager) : PageModel
{
    public List<string> Organizations { get; set; } = new();
    public List<Project> SelectedProjects { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Organization { get; set; }

    public string? SelectedOrganization { get; set; }

    [BindProperty]
    public Update NewUpdate { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return RedirectToPage("/Account/Login");
        }

        if (user.IsAdmin)
        {
            return RedirectToPage("/Projects/Index");
        }

        await LoadPageData(user.Id, Organization);

        return Page();
    }

    public async Task<IActionResult> OnPostAddHoursAsync(int projectId, string organization)
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return RedirectToPage("/Account/Login");
        }

        if (user.IsAdmin)
        {
            return RedirectToPage("/Projects/Index");
        }

        ModelState.Remove("NewUpdate.StudentID");
        ModelState.Remove("NewUpdate.ProjectID");
        ModelState.Remove("NewUpdate.Project");

        var project = await context.Projects
            .FirstOrDefaultAsync(p => p.Id == projectId && p.StudentID == user.Id);

        if (project == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            await LoadPageData(user.Id, organization);
            return Page();
        }

        NewUpdate.ProjectID = project.Id;
        NewUpdate.StudentID = user.Id;
        NewUpdate.IsWorkshop = false;

        context.Updates.Add(NewUpdate);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index", new { organization });
    }

    private async Task LoadPageData(string studentId, string? selectedOrganization)
    {
        var projects = await context.Projects
            .Include(p => p.Updates)
            .Where(p => p.StudentID == studentId)
            .OrderBy(p => p.Organization)
            .ThenBy(p => p.Title)
            .ToListAsync();

        Organizations = projects
            .Select(p => p.Organization)
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .Distinct()
            .ToList();

        SelectedOrganization = selectedOrganization;

        if (!string.IsNullOrWhiteSpace(selectedOrganization))
        {
            SelectedProjects = projects
                .Where(p => p.Organization == selectedOrganization)
                .ToList();
        }
    }
}