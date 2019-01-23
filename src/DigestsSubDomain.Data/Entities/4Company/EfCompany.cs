using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Abstract;
using Digests.Data.EfCore.Entities._4House;

namespace Digests.Data.EfCore.Entities._4Company
{
    public class EfCompany : BaseEntity
    {
        public string Name { get; set; }
        public List<EfHouse> EfHouses { get; set; }


        #region NavigationProp

        [ForeignKey("EfCompanyDetailsId")]
        public EfCompanyDetail EfCompanyDetail { get; set; }
        public int EfCompanyDetailsId { get; set; }

        #endregion
    }
}