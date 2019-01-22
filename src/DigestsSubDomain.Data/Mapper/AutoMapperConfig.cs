using AutoMapper;
using Digests.Core.Model._4Company;
using Digests.Core.Model._4House;
using Digests.Data.EfCore.Entities._4Company;
using Digests.Data.EfCore.Entities._4House;

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