using BlazorSchoolManager.Application.Responses.Identity;
using BlazorSchoolManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorSchoolManager.Application.Interfaces.Chat;
using BlazorSchoolManager.Application.Models.Chat;

namespace BlazorSchoolManager.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}