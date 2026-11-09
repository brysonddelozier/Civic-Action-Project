using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CivicAction.Data;
using CivicAction.Models;

namespace CivicAction.Pages.Updates
{
    public class EditModel : PageModel
    {
        private readonly CivicAction.Data.CivicActionContext _context;
        private readonly UserManager<AppUser> _userManager;

        public EditModel(CivicAction.Data.CivicActionContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Update Update { get; set; } = default!;

        //public Project Project { get; set; } = default!;

        public AppUser CurrentUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (id == null)
            {
                return NotFound();
            }

            var update =  await _context.Updates.FirstOrDefaultAsync(m => m.Id == id);
            if (update == null)
            {
                return NotFound();
            }
            Update = update;            

           ViewData["VerificationId"] = new SelectList(_context.Verifications, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            /*var project =  await _context.Projects.FirstOrDefaultAsync(p => p.Id == Update.ProjectId);
            if (project == null)
            {
                return NotFound();
            }*/

            //Console.WriteLine("Line 80 value here: " + NumRemainingUpdates(project.Id));

            if (await NumRemainingUpdates(Update.ProjectId ?? -1) == 1)
            {
                var project =  await _context.Projects.FirstOrDefaultAsync(p => p.Id == Update.ProjectId);
                if (project == null)
                {
                    return NotFound();
                }
                project.Status = Status.Approved;
                _context.Attach(Update).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            _context.Attach(Update).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UpdateExists(Update.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UpdateExists(int id)
        {
            return _context.Updates.Any(e => e.Id == id);
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(p => p.Id == id);
        }        

        private async Task<int> NumRemainingUpdates(int projectId)
        {
            return await _context.Updates
                .AsNoTracking()
                .CountAsync(u => u.ProjectId == projectId && u.Status == Status.Pending);
        }
    }
}
