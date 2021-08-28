
using BlazorSchoolManager.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace BlazorSchoolManager.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}