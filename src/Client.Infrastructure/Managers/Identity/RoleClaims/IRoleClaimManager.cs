using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorSchoolManager.Application.Requests.Identity;
using BlazorSchoolManager.Application.Responses.Identity;
using BlazorSchoolManager.Shared.Wrapper;

namespace BlazorSchoolManager.Client.Infrastructure.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}