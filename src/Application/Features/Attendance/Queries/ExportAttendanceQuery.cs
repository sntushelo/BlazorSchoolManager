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

namespace BlazorSchoolManager.Application.Features.Attendance.Queries
{
    public class ExportAttendanceQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportAttendanceQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportAttendanceQueryHandler : IRequestHandler<ExportAttendanceQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportAttendanceQueryHandler> _localizer;

        public ExportAttendanceQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportAttendanceQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportAttendanceQuery request, CancellationToken cancellationToken)
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
            }, sheetName: _localizer["Attendance"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}