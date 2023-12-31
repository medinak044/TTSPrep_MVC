using System.Security.Claims;

namespace TTSPrep_MVC.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
