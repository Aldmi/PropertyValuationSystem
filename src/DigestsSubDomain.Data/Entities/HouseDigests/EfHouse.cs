using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Abstract;

namespace DigestsSubDomain.Data.Entities.HouseDigests
{
    public class EfHouse : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string City { get; set; }                // Город
        [Required]
        public string District { get; set; }            // Район
        [Required]
        public string Street { get; set; }              // Улица
        [Required]
        public string Number { get; set; }              // Дом

        public int? Year { get; set; }                  // Год постройки
        public string MetroStation { get; set; }        // Ближайшая станция метро
        public string Geo { get; set; }                 // гео координаты

        #region NavigationProp

        [ForeignKey("WallMaterialId")]
        public EfWallMaterial WallMaterial { get; set; }
        public int WallMaterialId { get; set; }

        #endregion
    }
}