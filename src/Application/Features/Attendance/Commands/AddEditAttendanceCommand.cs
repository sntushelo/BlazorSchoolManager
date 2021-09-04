using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BlazorSchoolManager.Domain.Entities;
using BlazorSchoolManager.Application.Interfaces.Repositories;
using BlazorSchoolManager.Application.Interfaces.Services;
using BlazorSchoolManager.Application.Requests;
using BlazorSchoolManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolManager.Application.Features.Attendance.Commands
{
    public partial class AddEditAttendanceCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public int LessonId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public bool IsPresent { get; set; }
        [Required]
        public bool IsLate { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    internal class AddEditAttendanceCommandHandler : IRequestHandler<AddEditAttendanceCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<AddEditAttendanceCommandHandler> _localizer;

        public AddEditAttendanceCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService, IStringLocalizer<AddEditAttendanceCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditAttendanceCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Domain.Entities.Attendance>().Entities.Where(p => p.Id != command.Id)
                .AnyAsync(p => p.LessonId  == command.LessonId && p.StudentId == command.StudentId, cancellationToken))
            {
                return await Result<int>.FailAsync(_localizer["Barcode already exists."]);
            }

            if (command.Id == 0)
            {
                var attendance = _mapper.Map<Domain.Entities.Attendance>(command);
                await _unitOfWork.Repository<Domain.Entities.Attendance>().AddAsync(attendance);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(attendance.Id, _localizer["Attendance Saved"]);
            }
            else
            {
                var attendance = await _unitOfWork.Repository<Domain.Entities.Attendance>().GetByIdAsync(command.Id);
                if (attendance != null)
                {
                    attendance.LessonId = command.LessonId;
                    attendance.StudentId = command.StudentId;
                    attendance.IsPresent = command.IsPresent;
                    attendance.IsLate = command.IsLate;

                    await _unitOfWork.Repository<Domain.Entities.Attendance>().UpdateAsync(attendance);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(attendance.Id, _localizer["Attendance Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Attendance Not Found!"]);
                }
            }
        }
    }
}