using System.Security.Claims;

namespace WebApi;

public static class Extensions
{
    public static long GetId(this ClaimsPrincipal user)
    {
        var claim = user.Claims.FirstOrDefault(c => c.Type == "UserId");

        return long.Parse(claim?.Value ?? "0");
    }
}