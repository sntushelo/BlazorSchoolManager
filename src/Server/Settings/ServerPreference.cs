using System.Linq;
using BlazorSchoolManager.Shared.Constants.Localization;
using BlazorSchoolManager.Shared.Settings;

namespace BlazorSchoolManager.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}