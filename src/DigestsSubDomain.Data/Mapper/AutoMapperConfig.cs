using System;
using System.Collections.Generic;
using AutoMapper;
using Digests.Core.Model._4Company;
using Digests.Core.Model._4House;
using Digests.Data.EfCore.Entities._4Company;
using Digests.Data.EfCore.Entities._4House;

namespace Digests.Data.EfCore.Mapper
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static void Register()
        {
            if (Mapper != null)
                throw new InvalidOperationException("Метод регистрации AutoMapper не может быть вызванн повторно");

            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EfHouse, House>()
                        .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                        .ForMember(dest => dest.MetroStation, opt => opt.MapFrom(src => src.MetroStation))
                        .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                        .ForMember(dest => dest.WallMaterial, opt => opt.MapFrom(src => src.WallMaterial))
                        .ReverseMap();

                    cfg.CreateMap<EfWallMaterial, WallMaterial>()
                        .ConstructUsing(src => new WallMaterial(src.Name, src.IsShared))
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                        .ForMember(dest => dest.IsShared, opt => opt.MapFrom(src => src.IsShared));
                    cfg.CreateMap<WallMaterial, EfWallMaterial>()
                        .ConstructUsing(src => new EfWallMaterial{Name = src.Name, IsShared = src.IsShared})
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                        .ForMember(dest => dest.IsShared, opt => opt.MapFrom(src => src.IsShared))
                        .ForMember(dest => dest.Houses, opt => opt.Ignore());
                      


                    //cfg.CreateMap<EfWallMaterial, WallMaterial>()
                    //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    //    .ForMember(dest => dest.IsShared, opt => opt.MapFrom(src => src.IsShared));
                    //cfg.CreateMap<WallMaterial, EfWallMaterial>()
                    //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    //    .ForMember(dest => dest.IsShared, opt => opt.MapFrom(src => src.IsShared))
                    //    .ForMember(dest => dest.Houses, opt => opt.Ignore());

                    cfg.CreateMap<EfCompany, Company>()
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                        .ForMember(dest => dest.CompanyDetails, opt => opt.MapFrom(src => src.CompanyDetails))
                        .ForMember(dest => dest.GetHouses, opt => opt.MapFrom(src => src.Houses));
                    cfg.CreateMap<Company, EfCompany>()
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                        .ForMember(dest => dest.CompanyDetails, opt => opt.MapFrom(src => src.CompanyDetails))
                        .ForMember(dest => dest.Houses, opt => opt.MapFrom(src => src.GetHouses));
                });
            Mapper = config.CreateMapper();
        }


        public static void AssertConfigurationIsValid()
        {
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}