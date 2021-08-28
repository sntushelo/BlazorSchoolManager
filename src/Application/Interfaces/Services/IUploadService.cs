using BlazorSchoolManager.Application.Requests;

namespace BlazorSchoolManager.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}