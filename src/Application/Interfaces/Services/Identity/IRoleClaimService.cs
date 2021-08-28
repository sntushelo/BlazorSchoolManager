using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorSchoolManager.Application.Interfaces.Common;
using BlazorSchoolManager.Application.Requests.Identity;
using BlazorSchoolManager.Application.Responses.Identity;
using BlazorSchoolManager.Shared.Wrapper;

namespace BlazorSchoolManager.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}