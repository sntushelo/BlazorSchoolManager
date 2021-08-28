using AutoMapper;
using BlazorSchoolManager.Application.Features.Documents.Commands.AddEdit;
using BlazorSchoolManager.Application.Features.Documents.Queries.GetById;
using BlazorSchoolManager.Domain.Entities.Misc;

namespace BlazorSchoolManager.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}