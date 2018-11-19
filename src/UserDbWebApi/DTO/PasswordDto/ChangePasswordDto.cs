using System.ComponentModel.DataAnnotations;

namespace UserDbWebApi.DTO.PasswordDto
{
    public class ChangePasswordDto
    {
        public string UserId { get; set; }

        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "NewPassword не может быть NULL")]
        public string NewPassword { get; set; }
    }
}