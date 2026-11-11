using CivicAction.Data;
using CivicAction.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CivicAction.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly CivicAction.Data.CivicActionContext _context;
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(CivicAction.Data.CivicActionContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Project> Project { get;set; } = default!;
        public AppUser? CurrentUser {get; set;}

        public async Task OnGetAsync()
        {   
            CurrentUser = await _userManager.GetUserAsync(User);
            string? userId = _userManager.GetUserId(User);
            IQueryable<Project> projectsIQ = from p in _context.Projects select p;
            if (!CurrentUser.IsAdmin)
            {
                projectsIQ = projectsIQ.Where(p => p.StudentIds.Contains(userId));
            }
            Project = await projectsIQ.ToListAsync();
        }
    }
}
