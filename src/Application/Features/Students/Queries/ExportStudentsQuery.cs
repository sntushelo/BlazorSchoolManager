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

namespace BlazorSchoolManager.Application.Features.Students.Queries
{
    public class ExportStudentsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportStudentsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportStudentsQueryHandler : IRequestHandler<ExportStudentsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportStudentsQueryHandler> _localizer;

        public ExportStudentsQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportStudentsQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportStudentsQuery request, CancellationToken cancellationToken)
        {
            //var venueFilterSpec = new StudentFilterSpecification(request.SearchString);
            var venues = await _unitOfWork.Repository<Student>().Entities
                //.Specify(venueFilterSpec)
                .ToListAsync( cancellationToken);
            var data = await _excelService.ExportAsync(venues, mappers: new Dictionary<string, Func<Student, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["FirstName"], item => item.FirstName },
                { _localizer["LastName"], item => item.LastName },
                { _localizer["Age"], item => item.Age},
                { _localizer["Description"], item => item.Description },
                { _localizer["Gender"], item => item.Gender}
            }, sheetName: _localizer["Students"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}