using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CivicAction.Pages;

public class LogoutModel(IHttpContextAccessor httpContext) : PageModel
{
    public IActionResult OnPost()
    {
        httpContext.HttpContext!.Session.Clear();
        return RedirectToPage("/Login");
    }
}