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
using System;

namespace BlazorSchoolManager.Application.Features.Lessons.Commands
{
    public partial class AddEditLessonCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImageDataURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int TeacherId { get; set; }
        public int VenueId { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    internal class AddEditLessonCommandHandler : IRequestHandler<AddEditLessonCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<AddEditLessonCommandHandler> _localizer;

        public AddEditLessonCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService, IStringLocalizer<AddEditLessonCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditLessonCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Lesson>().Entities.Where(p => p.Id != command.Id)
                .AnyAsync(p => p.Name == command.Name, cancellationToken))
            {
                return await Result<int>.FailAsync(_localizer["Barcode already exists."]);
            }

            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"P-{command.Name}{uploadRequest.Extension}";
            }

            if (command.Id == 0)
            {
                var lesson = _mapper.Map<Lesson>(command);

                await _unitOfWork.Repository<Lesson>().AddAsync(lesson);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(lesson.Id, _localizer["Lesson Saved"]);
            }
            else
            {
                var lesson = await _unitOfWork.Repository<Lesson>().GetByIdAsync(command.Id);
                if (lesson != null)
                {
                    lesson.Name = command.Name ?? lesson.Name;
                    lesson.Description = command.Description ?? lesson.Description;
                    lesson.StartDate = command.StartDate;
                    lesson.EndDate = command.EndDate;
                    lesson.TeacherId = command.TeacherId;
                    lesson.VenueId = command.VenueId;

                    await _unitOfWork.Repository<Lesson>().UpdateAsync(lesson);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(lesson.Id, _localizer["Lesson Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Lesson Not Found!"]);
                }
            }
        }
    }
}