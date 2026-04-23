using CivicAction.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CivicAction.Models;
namespace CivicAction.Pages;

public class LoginModel(CivicActionContext context, IHttpContextAccessor httpContext) : PageModel
{
    [BindProperty] public string Email    { get; set; } = string.Empty;
    [BindProperty] public string Password { get; set; } = string.Empty;
    public string? Error { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
{
    var account = await context.Accounts
    .FirstOrDefaultAsync(a => a.Email == Email && a.Password == Password);

    if (account is null)
    {
    Error = "Invalid email or password.";
    return Page();
}

    httpContext.HttpContext!.Session.SetInt32("AccountId", account.Id);
    httpContext.HttpContext!.Session.SetString("IsAdmin", account.IsAdmin.ToString());
    return RedirectToPage("/Index");
}
}