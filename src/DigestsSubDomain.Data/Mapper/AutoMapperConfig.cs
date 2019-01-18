using AutoMapper;
using Digests.Core.Model;
using Digests.Core.Model.House;
using Digests.Data.EfCore.Entities;
using Digests.Data.EfCore.Entities.HouseDigests;

namespace Digests.Data.EfCore.Mapper
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