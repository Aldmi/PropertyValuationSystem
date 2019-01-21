using System;
using Shared.Kernel.ForDomain;

namespace Digests.Core.Model._4House
{
    public class House : DomainValueObject<House>
    {
        #region prop

        public string City { get; set; }                // Город
        public string District { get; set; }            // Район
        public string Street { get; set; }              // Улица
        public string Number { get; set; }              // Дом

        public int? Year { get; set; }                  // Год постройки
        public string MetroStation { get; set; }        // Ближайшая станция метро
        public string Geo { get; set; }                 // гео координаты
        public WallMaterial WallMaterial { get; set; }

        #endregion



        #region ctor

        public House(string city, string district, string street, string number, int? year, string metroStation, string geo, WallMaterial wallMaterial)
        {
            //TODO: добавить валидацйию
            City = city;
            District = district;
            Street = street;
            Number = number;
            Year = year;
            MetroStation = metroStation;
            Geo = geo;
            WallMaterial = wallMaterial;
        }

        #endregion



        #region OvverideMembers

        protected override bool EqualsCore(House other)
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