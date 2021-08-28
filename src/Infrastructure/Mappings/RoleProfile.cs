using AutoMapper;
using BlazorSchoolManager.Infrastructure.Models.Identity;
using BlazorSchoolManager.Application.Responses.Identity;

namespace BlazorSchoolManager.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}