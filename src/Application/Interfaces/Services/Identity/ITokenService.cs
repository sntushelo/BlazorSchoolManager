using BlazorSchoolManager.Application.Interfaces.Common;
using BlazorSchoolManager.Application.Requests.Identity;
using BlazorSchoolManager.Application.Responses.Identity;
using BlazorSchoolManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}