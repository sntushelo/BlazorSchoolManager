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

namespace BlazorSchoolManager.Application.Features.Teachers.Queries
{
    public class ExportTeachersQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportTeachersQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportTeachersQueryHandler : IRequestHandler<ExportTeachersQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportTeachersQueryHandler> _localizer;

        public ExportTeachersQueryHandler(IExcelService excelService, 
            IUnitOfWork<int> unitOfWork, IStringLocalizer<ExportTeachersQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportTeachersQuery request, CancellationToken cancellationToken)
        {
            //var teacherFilterSpec = new TeacherFilterSpecification(request.SearchString);
            var teachers = await _unitOfWork.Repository<Teacher>().Entities
                //.Specify(teacherFilterSpec)
                .ToListAsync( cancellationToken);
            var data = await _excelService.ExportAsync(teachers, mappers: new Dictionary<string, Func<Teacher, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["FirstName"], item => item.FirstName },
                { _localizer["LastName"], item => item.LastName },
                { _localizer["Age"], item => item.Age},
                { _localizer["Description"], item => item.Description },
                { _localizer["Gender"], item => item.Gender}
            }, sheetName: _localizer["Teachers"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}