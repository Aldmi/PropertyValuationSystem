using AutoMapper;
using DigestsSubDomain.Core.Model;
using DigestsSubDomain.Core.Model.Digests.HouseDigests;
using DigestsSubDomain.Data.EfCore.Entities;
using DigestsSubDomain.Data.EfCore.Entities.HouseDigests;

namespace DigestsSubDomain.Data.EfCore.Mapper
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