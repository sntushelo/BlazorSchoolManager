using BlazorSchoolManager.Application.Interfaces.Common;
using BlazorSchoolManager.Application.Requests.Identity;
using BlazorSchoolManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Application.Interfaces.Services.Account
{
    public interface IAccountService : IService
    {
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId);

        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
    }
}