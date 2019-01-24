using System;
using Shared.Kernel.ForDomain;

namespace Digests.Core.Model._4House
{
    public class House : DomainEntity
    {  
        #region prop

        public Address Address { get;  }
        public int? Year { get;  }                  // Год постройки
        public string MetroStation { get; }         // Ближайшая станция метро
        public WallMaterial WallMaterial { get;  }

        #endregion



        #region ctor

        public House(Address address,  WallMaterial wallMaterial)
        {
            //TODO: добавить валидацию
            Address = address;
            WallMaterial = wallMaterial; //TODO: как проверять на допустимый материал стен.
        }

        public House(Address address, WallMaterial wallMaterial, int year, string metroStation)
            :this(address, wallMaterial)
        {
            //TODO: добавить валидацию
            Year = year;
            MetroStation = metroStation;
        }

        #endregion
    }
}