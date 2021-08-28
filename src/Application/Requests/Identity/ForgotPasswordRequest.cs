using System.ComponentModel.DataAnnotations;

namespace BlazorSchoolManager.Application.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}