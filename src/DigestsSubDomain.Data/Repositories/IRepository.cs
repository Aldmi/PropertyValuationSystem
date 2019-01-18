using Database.Abstract.Abstract;
using DigestsSubDomain.Core.Model;
using DigestsSubDomain.Core.Model.Digests.HouseDigests;

namespace DigestsSubDomain.Data.EfCore.Repositories
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