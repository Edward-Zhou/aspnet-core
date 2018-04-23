using Abp.Authorization;
using EdwardAbp.Authorization.Roles;
using EdwardAbp.Authorization.Users;

namespace EdwardAbp.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
