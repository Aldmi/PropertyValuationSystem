using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserDbWebApi.DTO.RolesDto
{
    public class RoleDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Name не может быть NULL")]
        public string Name { get; set; }

        public Dictionary<string, string> Claims { get; set; }
    }
}