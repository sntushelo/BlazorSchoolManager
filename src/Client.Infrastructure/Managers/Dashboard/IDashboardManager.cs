using BlazorSchoolManager.Shared.Wrapper;
using System.Threading.Tasks;
using BlazorSchoolManager.Application.Features.Dashboards.Queries.GetData;

namespace BlazorSchoolManager.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}