using System;
using Application.Dto._4Digests;
using AutoMapper;
using Digests.Core.Model._4Company;
using Digests.Core.Model._4House;

namespace Application.Mapper
{
    public class ApplicationAutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static void Register()
        {
            if (Mapper != null)
                throw new InvalidOperationException("Метод регистрации AutoMapper не может быть вызван повторно");

            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Company, CompanyDto>()
                        .ReverseMap();

                    cfg.CreateMap<CompanyDetails, CompanyDetailsDto>()
                        .ReverseMap();

                    cfg.CreateMap<House, HouseDto>()
                        .ReverseMap();

                    cfg.CreateMap<Address, AddressDto>()
                        .ReverseMap();

                    cfg.CreateMap<WallMaterial, WallMaterialDto>()
                        .ReverseMap();



                    //cfg.CreateMap<House, HouseDto>()
                    //    .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                    //    .ForMember(dest => dest.MetroStation, opt => opt.MapFrom(src => src.MetroStation))
                    //    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                    //    .ForMember(dest => dest.WallMaterial, opt => opt.MapFrom(src => src.WallMaterial))
                    //    .ReverseMap();



                });
            Mapper = config.CreateMapper();
        }


        public static void AssertConfigurationIsValid()
        {
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}