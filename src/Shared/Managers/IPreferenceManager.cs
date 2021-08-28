using BlazorSchoolManager.Shared.Settings;
using System.Threading.Tasks;
using BlazorSchoolManager.Shared.Wrapper;

namespace BlazorSchoolManager.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}