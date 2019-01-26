using Database.Abstract.Abstract;
using Digests.Core.Model;
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
    /// Доступ к материалам стен (СПРАВОЧНИК)
    /// </summary>
    public interface IWallMaterialRepository : IGenericDataRepository<WallMaterial>
    {
    }
}