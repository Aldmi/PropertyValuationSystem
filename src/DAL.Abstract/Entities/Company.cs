using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Abstract.Entities
{
    public class Company : EntityBase
    {
        public string Name { get; set; }
    }
}