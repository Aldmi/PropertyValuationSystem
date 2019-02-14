using CSharpFunctionalExtensions;
using Digests.Core.Model._4Company;
using FluentValidation;
using Newtonsoft.Json;
using Shared.Kernel.ForDomain;
using Shared.Kernel.Validators;

namespace Digests.Core.Model._4House
{
    public class Address : DomainValueObject<Address>
    {
        #region prop

        public string City { get; }                // Город
        public string District { get;  }            // Район
        public string Street { get; }               // Улица
        public string Number { get;  }              // Дом
        public string Geo { get;  }                 // гео координаты

        #endregion



        #region ctor

        [JsonConstructor]
        private Address(string city, string district, string street, string number, string geo)
        {
            City = city;
            District = district;
            Street = street;
            Number = number;
            Geo = geo;
        }

        #endregion



        #region Factory

        public static Result<Address, string> Create(string city, string district, string street, string number, string geo)
        {
            var address = new Address(city, district, street, number, geo);
            var addressValidator = new AddressValidator();
            var valRes = addressValidator.Validate(address);
            if (valRes.IsValid)
            {
                return Result.Ok<Address, string>(address);
            }
            var errors = valRes.ToString("~");
            return Result.Fail<Address, string>(errors);
        }


        private class AddressValidator : AbstractValidator<Address>
        {
            public AddressValidator()
            {
                RuleFor(x => x.City).SetValidator(new StringNotNullNotEmptyValidator());
                RuleFor(x => x.Street).SetValidator(new StringNotNullNotEmptyValidator());
                RuleFor(x => x.Number).SetValidator(new StringNotNullNotEmptyValidator());
            }
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