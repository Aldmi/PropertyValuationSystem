using Shared.Kernel.ForDomain;

namespace Application.Dto._4Digests
{
    public class AddressDto
    {
        #region prop

        public string City { get; set; }                // Город
        public string District { get; set; }            // Район
        public string Street { get; set; }               // Улица
        public string Number { get; set; }              // Дом
        public string Geo { get; set; }                 // гео координаты

        #endregion

    }
}