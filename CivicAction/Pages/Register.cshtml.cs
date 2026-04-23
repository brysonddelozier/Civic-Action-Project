using CivicAction.Data;
using CivicAction.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CivicAction.Pages;

public class RegisterModel(CivicActionContext context, IHttpContextAccessor httpContext) : PageModel
{
    [BindProperty] public string FirstMidName { get; set; } = string.Empty;
    [BindProperty] public string LastName     { get; set; } = string.Empty;
    [BindProperty] public string Email        { get; set; } = string.Empty;
    [BindProperty] public string Password     { get; set; } = string.Empty;
    [BindProperty] public string Confirm      { get; set; } = string.Empty;
    [BindProperty] public Grade  Grade        { get; set; }
    [BindProperty] public string School       { get; set; } = string.Empty;
    public string? Error { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Password != Confirm)
        {
            Error = "Passwords do not match.";
            return Page();
        }

        var emailTaken = await context.Accounts.AnyAsync(a => a.Email == Email);
        if (emailTaken)
        {
            Error = "An account with that email already exists.";
            return Page();
        }

        //var hasher  = new PasswordHasher<Account>();
        var account = new Account
        {
            FirstMidName = FirstMidName,
            LastName     = LastName,
            Email        = Email,
            Grade        = Grade,
            School       = School,
            IsAdmin      = false
        };

        //account.Password = hasher.HashPassword(account, Password);
        account.Password = Password;

        context.Accounts.Add(account);
        await context.SaveChangesAsync();

        // Log them in straight away
        httpContext.HttpContext!.Session.SetInt32("AccountId", account.Id);
        httpContext.HttpContext!.Session.SetString("IsAdmin", account.IsAdmin.ToString());

        return RedirectToPage("/Index");
    }
}