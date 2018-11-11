using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UserDbWebApi.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Компания не установлена")]
        public Company Company { get; set; }
    }
}
