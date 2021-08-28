using System.Text.Json;
using BlazorSchoolManager.Application.Interfaces.Serialization.Options;

namespace BlazorSchoolManager.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}