using BlazorSchoolManager.Application.Requests.Mail;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}