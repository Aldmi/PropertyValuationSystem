namespace DigestsSubDomain.Core.Model.Digests.HouseDigests
{
    public class House : EntityBase
    {
        public string City { get; set; }                // Город
        public string District { get; set; }            // Район
        public string Street { get; set; }              // Улица
        public string Number { get; set; }              // Дом

        public int? Year { get; set; }                  // Год постройки
        public string MetroStation { get; set; }        // Ближайшая станция метро
        public string Geo { get; set; }                 // гео координаты

        public WallMaterial WallMaterial { get; set; }
    }
}