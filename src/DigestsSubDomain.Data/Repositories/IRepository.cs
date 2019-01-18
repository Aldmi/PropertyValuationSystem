using Database.Abstract.Abstract;
using Digests.Core.Model;
using Digests.Core.Model.House;

namespace Digests.Data.EfCore.Repositories
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