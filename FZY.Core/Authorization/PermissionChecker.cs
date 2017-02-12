using Abp.Authorization;
using FZY.Authorization.Roles;
using FZY.MultiTenancy;
using FZY.Users;

namespace FZY.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
