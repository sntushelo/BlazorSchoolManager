using System.Collections.Generic;

namespace BlazorSchoolManager.Application.Responses.Identity
{
    public class GetAllUsersResponse
    {
        public IEnumerable<UserResponse> Users { get; set; }
    }
}