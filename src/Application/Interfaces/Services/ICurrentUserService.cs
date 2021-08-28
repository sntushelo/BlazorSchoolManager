using BlazorSchoolManager.Application.Interfaces.Common;

namespace BlazorSchoolManager.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}