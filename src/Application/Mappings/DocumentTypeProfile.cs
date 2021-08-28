using AutoMapper;
using BlazorSchoolManager.Application.Features.DocumentTypes.Commands.AddEdit;
using BlazorSchoolManager.Application.Features.DocumentTypes.Queries.GetAll;
using BlazorSchoolManager.Application.Features.DocumentTypes.Queries.GetById;
using BlazorSchoolManager.Domain.Entities.Misc;

namespace BlazorSchoolManager.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}