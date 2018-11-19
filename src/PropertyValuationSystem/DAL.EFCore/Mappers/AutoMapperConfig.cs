using AutoMapper;
using DAL.Abstract.Entities;
using DAL.Abstract.Entities.Digests.HouseDigests;
using DAL.EFCore.Entities;
using DAL.EFCore.Entities.HouseDigests;

namespace DAL.EFCore.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper { get; set; }

        public static void Register()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<House, EfHouse>().ReverseMap();
                    cfg.CreateMap<WallMaterial, EfWallMaterial>().ReverseMap();
                    cfg.CreateMap<Company, EfCompany>().ReverseMap();
                });
            Mapper = config.CreateMapper();
        }
    }
}