using Impacta.GarageTrack.System.Api.Domain.Kernel;
using System.Security.Claims;

namespace Impacta.GarageTrack.System.Api.Api.Kernel
{
    public static class HttpContextExtensions
    {
        public static UserSession? GetCurrentUser(this HttpContext httpContext)
        {
            var user = httpContext.User;
            if (user?.Identity?.IsAuthenticated != true)
            {
                return null;
            }

            var Id = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var Email = user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
            var Name = user.FindFirst(ClaimTypes.GivenName)?.Value ?? string.Empty;
            var CompanyId = long.Parse(user.FindFirst("CompanyId")?.Value ?? "0");
            var CompanyName = user.FindFirst("CompanyName")?.Value ?? string.Empty;
            var UserType = Enum.Parse<UserRole>(user.FindFirst(ClaimTypes.Role)?.Value ?? "User");

            return new UserSession(Id, CompanyId, CompanyName, Name, Email, UserType);
        }
    }
}
