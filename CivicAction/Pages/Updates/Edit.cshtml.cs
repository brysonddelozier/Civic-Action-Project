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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!ProjectHasMoreUpdates(Update.ProjectId ?? -1))
            {
                Console.Write("We reached line 61");
                var projectToUpdate = _context.Projects.Single(p => p.Id == Update.ProjectId);
                projectToUpdate.Status = Status.Approved;
                _context.Attach(projectToUpdate).State = EntityState.Modified;
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

        private bool ProjectHasMoreUpdates(int ProjectId)
        {
            var _projectUpdates = _context.Projects.Where(p => p.Id == ProjectId && p.Status == Status.Pending);
            return _projectUpdates.Any();
        }
    }
}
