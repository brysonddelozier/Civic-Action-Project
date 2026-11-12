using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CivicAction.Data;
using CivicAction.Models;

namespace CivicAction.Pages.Updates
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

        public IList<Update> Update { get;set; } = default!;
        public AppUser? CurrentUser { get; set; }

        public async Task OnGetAsync()
        {   
            CurrentUser = await _userManager.GetUserAsync(User);
            string? userId = _userManager.GetUserId(User);
            IQueryable<Update> updatesIQ = from u in _context.Updates select u;
            if (!CurrentUser.IsAdmin)
            {
                updatesIQ = updatesIQ.Where(u => u.StudentId == userId);
            }
            Update = await updatesIQ.Include(u => u.Project).ToListAsync();
            foreach (Update update in Update)
            {
                update.Student = await _userManager.FindByIdAsync(update.StudentId);
            }


        }
    }
}
