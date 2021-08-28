using AutoMapper;
using BlazorSchoolManager.Infrastructure.Models.Audit;
using BlazorSchoolManager.Application.Responses.Audit;

namespace BlazorSchoolManager.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}