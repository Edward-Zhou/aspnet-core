using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using EdwardAbp.Configuration.Dto;

namespace EdwardAbp.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : EdwardAbpAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
