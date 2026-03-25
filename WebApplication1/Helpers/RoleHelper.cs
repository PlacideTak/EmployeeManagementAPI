using System.Security.Claims;

namespace WebApplication1.Helpers
{
    public class RoleHelper : IRoleHelper
    {
        public bool IsAdmin(ClaimsPrincipal user)
        {
            return user.IsInRole(Roles.Admin);
        }

        public bool IsManager(ClaimsPrincipal user)
        {
            return user.IsInRole(Roles.Manager);
        }

        public bool IsEmployee(ClaimsPrincipal user)
        {
            return user.IsInRole(Roles.Employee);
        }

        public bool HasRole(ClaimsPrincipal user, string role)
        {
            return user.IsInRole(role);
        }

        public void RequireRole(ClaimsPrincipal user, string role)
        {
            if (!user.IsInRole(role))
            {
                throw new UnauthorizedAccessException($"Role {role} required");
            }
        }
    }
}
