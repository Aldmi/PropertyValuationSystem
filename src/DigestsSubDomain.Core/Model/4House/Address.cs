using Shared.Kernel.ForDomain;

namespace Digests.Core.Model._4House
{
    public class Address : DomainValueObject<Address>
    {
        #region prop

        public string City { get; set; }                // Город
        public string District { get; set; }            // Район
        public string Street { get; set; }              // Улица
        public string Number { get; set; }              // Дом
        public string Geo { get; set; }                 // гео координаты

        #endregion



        #region ctor

        public Address(string city, string district, string street, string number, string geo)
        {
            City = city;
            District = district;
            Street = street;
            Number = number;
            Geo = geo;
        }

        #endregion



        #region OvverideMembers
        protected override bool EqualsCore(Address other)
        {
            return City == other.City
                   && District == other.District
                   && Street == other.Street
                   && Number == other.Number
                   && Geo == other.Geo;
        }

        protected override int GetHashCodeCore()
        {
            int hashCode = City.GetHashCode();
            hashCode = (hashCode * 397) ^ District.GetHashCode();
            hashCode = (hashCode * 397) ^ Street.GetHashCode();
            hashCode = (hashCode * 397) ^ Number.GetHashCode();
            hashCode = (hashCode * 397) ^ Geo.GetHashCode();
            return hashCode;
        }
        #endregion
    }
}