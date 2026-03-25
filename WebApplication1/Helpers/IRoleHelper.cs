using System.Security.Claims;

namespace WebApplication1.Helpers
{
    public interface IRoleHelper
    {
        bool IsAdmin(ClaimsPrincipal user);
        bool IsManager(ClaimsPrincipal user);
        bool IsEmployee(ClaimsPrincipal user);
        bool HasRole(ClaimsPrincipal user, string role);
        void RequireRole(ClaimsPrincipal user, string role);
    }
}
