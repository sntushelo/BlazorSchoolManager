using AutoMapper;
using BlazorSchoolManager.Application.Interfaces.Chat;
using BlazorSchoolManager.Application.Models.Chat;
using BlazorSchoolManager.Infrastructure.Models.Identity;

namespace BlazorSchoolManager.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}