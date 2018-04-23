using System.Threading.Tasks;
using EdwardAbp.Configuration.Dto;

namespace EdwardAbp.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
