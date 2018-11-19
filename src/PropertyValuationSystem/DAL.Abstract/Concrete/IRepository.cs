using DAL.Abstract.Abstract;
using DAL.Abstract.Entities;
using DAL.Abstract.Entities.Digests.HouseDigests;


namespace DAL.Abstract.Concrete
{
    /// <summary>
    /// Доступ к Компаниям
    /// </summary>
    public interface ICompanyRepository : IGenericDataRepository<Company>
    {
    }

    /// <summary>
    /// Доступ к домам (СПРАВОЧНИК)
    /// </summary>
    public interface IHouseRepository : IGenericDataRepository<House>
    {
    }

    /// <summary>
    /// Доступ к материалам стен (СПРАВОЧНИК)
    /// </summary>
    public interface IWallMaterialRepository : IGenericDataRepository<WallMaterial>
    {
    }
}