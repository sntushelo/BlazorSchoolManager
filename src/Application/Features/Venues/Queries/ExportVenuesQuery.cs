using BlazorSchoolManager.Application.Interfaces.Repositories;
using BlazorSchoolManager.Application.Interfaces.Services;
using BlazorSchoolManager.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorSchoolManager.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BlazorSchoolManager.Application.Features.Venues.Queries
{
    public class ExportVenuesQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportVenuesQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportVenuesQueryHandler : IRequestHandler<ExportVenuesQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportVenuesQueryHandler> _localizer;

        public ExportVenuesQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportVenuesQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportVenuesQuery request, CancellationToken cancellationToken)
        {
            //var venueFilterSpec = new VenueFilterSpecification(request.SearchString);
            var venues = await _unitOfWork.Repository<Venue>().Entities
                //.Specify(venueFilterSpec)
                .ToListAsync( cancellationToken);
            var data = await _excelService.ExportAsync(venues, mappers: new Dictionary<string, Func<Venue, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Capacity"], item => item.Capacity },
                { _localizer["Description"], item => item.Description },
                { _localizer["IsOnline"], item => item.IsOnline }
            }, sheetName: _localizer["Venues"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}