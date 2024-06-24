using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Shared.Auth;

public static class TokenHelper
{
    public static int GetCompanyIdFromContext(HttpContext context)
    {
        var companyIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        if (companyIdClaim != null && int.TryParse(companyIdClaim.Value, out int companyId))
        {
            return companyId;
        }
        throw new Exception("Invalid Token");
    }
}