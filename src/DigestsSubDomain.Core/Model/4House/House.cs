using System;
using CSharpFunctionalExtensions;
using FluentValidation;
using Shared.Kernel.ForDomain;
using Shared.Kernel.Validators;

namespace Digests.Core.Model._4House
{
    public class House : DomainEntity
    {  
        #region prop

        public Address Address { get;  }
        public int? Year { get; }                   // Год постройки
        public string MetroStation { get; }         // Ближайшая станция метро
        public WallMaterial WallMaterial { get; private set; }

        #endregion



        #region ctor

        private House(Address address,  WallMaterial wallMaterial)
        {
            //TODO: добавить валидацию
            Address = address;
            WallMaterial = wallMaterial; //TODO: как проверять на допустимый материал стен.
        }

        private House(Address address, WallMaterial wallMaterial, int year, string metroStation)
            :this(address, wallMaterial)
        {
            //TODO: добавить валидацию
            Year = year;
            MetroStation = metroStation;
        }

        #endregion



        #region Factory

        public static Result<House, string> Create(Address address, WallMaterial wallMaterial)
        {
            return Create(address, wallMaterial, 0, null);
        }


        public static Result<House, string> Create(Address address, WallMaterial wallMaterial, int year, string metroStation)
        {
            var house = new House(address, wallMaterial, year, metroStation);
            var houseValidator = new HouseValidator();
            var valRes = houseValidator.Validate(house);
            if (valRes.IsValid)
            {
                return Result.Ok<House, string>(house);
            }
            var errors = valRes.ToString("~");
            return Result.Fail<House, string>(errors);
        }


        private class HouseValidator : AbstractValidator<House>
        {
            public HouseValidator()
            {
                RuleFor(x => x.Address).NotNull();
            }
        }

        #endregion




        public void ChangeWallMaterial(WallMaterial wm)
        {
            WallMaterial = wm;
        }
    }
}