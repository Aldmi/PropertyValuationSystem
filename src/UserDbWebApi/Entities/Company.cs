using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserDbWebApi.Entities
{
    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [Required]
        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}