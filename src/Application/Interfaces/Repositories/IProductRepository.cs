using System.Threading.Tasks;

namespace BlazorSchoolManager.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<bool> IsBrandUsed(int brandId);
    }
}