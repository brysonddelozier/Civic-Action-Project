using CivicAction.Data;
using CivicAction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CivicAction.Pages.Projects;

public class IndexModel(CivicActionContext context, IHttpContextAccessor httpContext) : PageModel
{
    public IList<Project> Projects { get; private set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        var accountId = httpContext.HttpContext!.Session.GetInt32("AccountId");
        if (accountId == null)
            return RedirectToPage("/Login");

        var isAdmin = httpContext.HttpContext.Session.GetString("IsAdmin") == "True";

        Projects = isAdmin
            ? await context.Projects.Include(p => p.Student).ToListAsync()
            : await context.Projects.Include(p => p.Student)
                .Where(p => p.StudentID == accountId)
                .ToListAsync();

        return Page();
    }
}