using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CivicAction.Pages;

public class IndexModel(IHttpContextAccessor httpContext) : PageModel
{
    public IActionResult OnGet()
    {
        if (httpContext.HttpContext!.Session.GetInt32("AccountId") == null)
            return RedirectToPage("/Login");

        return Page();
    }
}
