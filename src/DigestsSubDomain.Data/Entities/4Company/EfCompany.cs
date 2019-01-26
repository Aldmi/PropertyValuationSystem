using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Abstract;
using Digests.Core.Model._4Company;
using Digests.Core.Model._4House;
using Digests.Data.EfCore.Entities._4House;
using Newtonsoft.Json;

namespace Digests.Data.EfCore.Entities._4Company
{
    [Table("Company")]
    public class EfCompany : BaseEntity
    {
        public string Name { get; set; }
        public CompanyDetails CompanyDetails { get; set; }
        public List<EfHouse> Houses { get; set; } = new List<EfHouse>();
    }
}