using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace EdwardAbp.Controllers
{
    public abstract class EdwardAbpControllerBase: AbpController
    {
        protected EdwardAbpControllerBase()
        {
            LocalizationSourceName = EdwardAbpConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
