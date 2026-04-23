using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CivicAction.Data;
using CivicAction.Models;

namespace CivicAction.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly CivicAction.Data.CivicActionContext _context;

        public CreateModel(CivicAction.Data.CivicActionContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //ViewData["StudentID"] = new SelectList(_context.Accounts, "Id", "FirstMidName");
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine($"StudentID Posted: {Project?.StudentID}");
            Console.WriteLine($"Description: {Project?.Description}");
            Console.WriteLine($"Hours: {Project?.Hours}");
            Console.WriteLine($"Organization: {Project?.Organization}");
            Console.WriteLine($"Start: {Project?.Start}");
            Console.WriteLine($"End: {Project?.End}");
            
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                    }
                }
                //ViewData["StudentID"] = new SelectList(_context.Accounts, "Id", "FirstMidName");
                return Page();
            }

            try
            {
                Project.StudentID = (int)HttpContext.Session.GetInt32("AccountId")!;
                Project.IsApproved = false;
                _context.Projects.Add(Project);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Save Error: {ex.Message}");
                ModelState.AddModelError("", $"Error saving project: {ex.Message}");
                ViewData["StudentID"] = new SelectList(_context.Accounts.ToList(), "Id", "FirstMidName");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
