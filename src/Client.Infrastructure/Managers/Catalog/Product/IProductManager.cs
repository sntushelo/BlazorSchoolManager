using BlazorSchoolManager.Application.Features.Products.Commands.AddEdit;
using BlazorSchoolManager.Application.Features.Products.Queries.GetAllPaged;
using BlazorSchoolManager.Application.Requests.Catalog;
using BlazorSchoolManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}