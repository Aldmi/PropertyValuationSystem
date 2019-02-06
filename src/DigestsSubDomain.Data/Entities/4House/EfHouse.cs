using System.ComponentModel.DataAnnotations.Schema;
using Database.Abstract;
using Digests.Core.Model._4House;
using Digests.Data.EfCore.Entities._4Company;
using Newtonsoft.Json;

namespace Digests.Data.EfCore.Entities._4House
{
    /// <summary>
    /// Дом.
    /// Промежуточная таблица для Many2Many(EfWallMaterial, EfCompany) 
    /// </summary>
    [Table("House")]
    public class EfHouse : BaseEntity
    {
        #region prop
        public Address Address { get; set; }
        public int? Year { get; set; }
        public string MetroStation { get; set; }

        #endregion


        #region NavigationProp

        [ForeignKey("WallMaterialId")] public EfWallMaterial WallMaterial { get; set; }
        public long? WallMaterialId { get; set; }

        [ForeignKey("EfCompanyId")] public EfCompany EfCompany { get; set; }
        public long EfCompanyId { get; set; }

        #endregion
    }
}