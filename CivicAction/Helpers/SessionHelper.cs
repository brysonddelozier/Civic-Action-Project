namespace CivicAction.Helpers;

public static class SessionHelper
{
    public static int? GetAccountId(IHttpContextAccessor ctx)
        => ctx.HttpContext?.Session.GetInt32("AccountId");

    public static bool IsLoggedIn(IHttpContextAccessor ctx)
        => GetAccountId(ctx) is not null;

    public static bool IsAdmin(IHttpContextAccessor ctx)
        => ctx.HttpContext?.Session.GetString("IsAdmin") == "True";
}