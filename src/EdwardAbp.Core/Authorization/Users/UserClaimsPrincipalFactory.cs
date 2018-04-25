using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using EdwardAbp.Authorization.Roles;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace EdwardAbp.Authorization.Users
{
    public class UserClaimsPrincipalFactory : AbpUserClaimsPrincipalFactory<User, Role>
    {
        public UserClaimsPrincipalFactory(
            UserManager userManager,
            RoleManager roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(
                  userManager,
                  roleManager,
                  optionsAccessor)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var claims = await base.CreateAsync(user);
            claims.Identities.First().AddClaim(new Claim("ou","1"));
            return claims;
        }
    }
}
