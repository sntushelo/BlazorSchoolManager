using BlazorSchoolManager.Application.Extensions;
using BlazorSchoolManager.Application.Interfaces.Repositories;
using BlazorSchoolManager.Domain.Entities;
using BlazorSchoolManager.Shared.Wrapper;
using MediatR;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Application.Features.Venues.Queries
{
    public class GetVenuesQuery : IRequest<PaginatedResult<GetVenuesResponsePaged>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetVenuesQuery(int pageNumber, int pageSize, string searchString, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }

    internal class GetVenuesQueryHandler : IRequestHandler<GetVenuesQuery, PaginatedResult<GetVenuesResponsePaged>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetVenuesQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetVenuesResponsePaged>> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            if (request.OrderBy?.Any() != true)
            {
                return await _unitOfWork.Repository<Venue>()
                    .Entities
                    .Select(v => VenueConverter.ToModel(v))
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            }
            
            var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
            return await _unitOfWork.Repository<Venue>()
                .Entities
                .OrderBy(ordering) // require system.linq.dynamic.core
                .Select(v => VenueConverter.ToModel(v))
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}