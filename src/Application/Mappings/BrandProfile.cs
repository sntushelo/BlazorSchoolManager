using AutoMapper;
using BlazorSchoolManager.Application.Features.Brands.Commands.AddEdit;
using BlazorSchoolManager.Application.Features.Brands.Queries.GetAll;
using BlazorSchoolManager.Application.Features.Brands.Queries.GetById;
using BlazorSchoolManager.Domain.Entities.Catalog;

namespace BlazorSchoolManager.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}