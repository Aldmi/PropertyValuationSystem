using Database.Abstract.Abstract;
using Digests.Core.Model;
using Digests.Core.Model.Shared;
using Digests.Core.Model._4Company;
using Digests.Core.Model._4House;

namespace Digests.Data.Abstract
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

    /// <summary>
    /// Доступ к общим материалам (СПРАВОЧНИК)
    /// </summary>
    public interface ISharedWallMaterialsRepository : IGenericDataRepository<WallMaterial>
    {
    }
}