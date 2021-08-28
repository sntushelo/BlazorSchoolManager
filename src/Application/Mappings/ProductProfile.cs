using AutoMapper;
using BlazorSchoolManager.Application.Features.Products.Commands.AddEdit;
using BlazorSchoolManager.Domain.Entities.Catalog;

namespace BlazorSchoolManager.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}