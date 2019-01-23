using System.ComponentModel.DataAnnotations.Schema;
using Database.Abstract;
using Digests.Data.EfCore.Entities._4Company;

namespace Digests.Data.EfCore.Entities._4House
{
    public class EfHouse : BaseEntity
    {
        public string City { get; set; }                // Город
        public string District { get; set; }            // Район
        public string Street { get; set; }              // Улица
        public string Number { get; set; }              // Дом

        public int? Year { get; set; }                  // Год постройки
        public string MetroStation { get; set; }        // Ближайшая станция метро
        public string Geo { get; set; }                 // гео координаты


        #region NavigationProp

        [ForeignKey("WallMaterialId")]
        public EfWallMaterial WallMaterial { get; set; }
        public int WallMaterialId { get; set; }

        [ForeignKey("EfCompanyId")]
        public EfCompany EfCompany { get; set; }
        public int EfCompanyId { get; set; }

        #endregion
    }
}