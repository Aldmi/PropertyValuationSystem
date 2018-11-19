using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using UserDbWebApi.Entities;

namespace UserDbWebApi.DTO.UserDto
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Компания не установлена")]
        public string UserName { get; set; }

        public string Password { get; set; }
        public CompanyDto Company { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}