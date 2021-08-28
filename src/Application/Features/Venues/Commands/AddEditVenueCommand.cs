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

namespace BlazorSchoolManager.Application.Features.Venues.Commands
{
    public partial class AddEditVenueCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImageDataURL { get; set; }
        public bool IsOnline { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    internal class AddEditVenueCommandHandler : IRequestHandler<AddEditVenueCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<AddEditVenueCommandHandler> _localizer;

        public AddEditVenueCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService, IStringLocalizer<AddEditVenueCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditVenueCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Venue>().Entities.Where(p => p.Id != command.Id)
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
                var venue = _mapper.Map<Venue>(command);
                if (uploadRequest != null)
                {
                    venue.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                }
                await _unitOfWork.Repository<Venue>().AddAsync(venue);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(venue.Id, _localizer["Venue Saved"]);
            }
            else
            {
                var venue = await _unitOfWork.Repository<Venue>().GetByIdAsync(command.Id);
                if (venue != null)
                {
                    venue.Name = command.Name ?? venue.Name;
                    venue.Description = command.Description ?? venue.Description;
                    venue.Capacity = (command.Capacity == 0) ? venue.Capacity: command.Capacity;
                    venue.IsOnline = command.IsOnline;
                    if (uploadRequest != null)
                    {
                        venue.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                    }
                    await _unitOfWork.Repository<Venue>().UpdateAsync(venue);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(venue.Id, _localizer["Venue Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Venue Not Found!"]);
                }
            }
        }
    }
}