using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BlazorSchoolManager.Application.Interfaces.Repositories;
using BlazorSchoolManager.Application.Interfaces.Services;
using BlazorSchoolManager.Application.Requests;
using BlazorSchoolManager.Domain.Entities;
using BlazorSchoolManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolManager.Application.Features.Teachers.Commands
{
    public partial class AddEditTeacherCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImageDataURL { get; set; }
        public char Gender { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    internal class AddEditTeacherCommandHandler : IRequestHandler<AddEditTeacherCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<AddEditTeacherCommandHandler> _localizer;

        public AddEditTeacherCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService, IStringLocalizer<AddEditTeacherCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditTeacherCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Teacher>().Entities.Where(p => p.Id != command.Id)
                .AnyAsync(p => p.FirstName == command.FirstName && p.LastName == command.LastName, cancellationToken))
            {
                return await Result<int>.FailAsync(_localizer["Barcode already exists."]);
            }

            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"P-{command.FirstName} {command.LastName}{uploadRequest.Extension}";
            }

            if (command.Id == 0)
            {
                var teacher = _mapper.Map<Teacher>(command);
                if (uploadRequest != null)
                {
                    teacher.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                }
                await _unitOfWork.Repository<Teacher>().AddAsync(teacher);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(teacher.Id, _localizer["Teacher Saved"]);
            }
            else
            {
                var teacher = await _unitOfWork.Repository<Teacher>().GetByIdAsync(command.Id);
                if (teacher != null)
                {
                    teacher.FirstName = command.FirstName ?? teacher.FirstName;
                    teacher.LastName = command.LastName ?? teacher.LastName;
                    teacher.Description = command.Description ?? teacher.Description;
                    teacher.Age = (command.Age == 0) ? teacher.Age: command.Age;
                    teacher.Gender = command.Gender;
                    if (uploadRequest != null)
                    {
                        teacher.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                    }
                    await _unitOfWork.Repository<Teacher>().UpdateAsync(teacher);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(teacher.Id, _localizer["Teacher Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Teacher Not Found!"]);
                }
            }
        }
    }
}