using BlazorSchoolManager.Application.Features.Documents.Commands.AddEdit;
using BlazorSchoolManager.Application.Features.Documents.Queries.GetAll;
using BlazorSchoolManager.Application.Requests.Documents;
using BlazorSchoolManager.Shared.Wrapper;
using System.Threading.Tasks;
using BlazorSchoolManager.Application.Features.Documents.Queries.GetById;

namespace BlazorSchoolManager.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}