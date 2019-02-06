using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
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
        Task<Company> GetCompanyByNameAsync(string companyName);                                        //Вернуть компанию

        #region House
        Task<IReadOnlyList<House>> GetAllHouseAsync(long companyId);                                     //Вернуть все дома компании
        Task<House> GetHouseAsync(long companyId, long houseId);                                                         //Вернуть дом компании
        Task<House> GetHouseAsync(long companyId, Address address);                                                      //Вернуть дом компании по адрессу                  
        Task<IReadOnlyList<WallMaterial>> GetAllWallMaterialsAsync(long companyId);                      //Вернуть все материалы стен этой компанией (IsShared= false)
        Task<IReadOnlyList<House>> GetHouseOfWallMaterialAsync(long companyId, string nameWallmaterial); //Вернуть все дома фирмы из заданного материала стен

        Task<bool> UpdateCompanyDetailsAsync(long companyId, CompanyDetails newCompanyDetails);

        Task<bool> AddHouseInCompanyAsync(long companyId, House house);
        Task<bool> RemoveHouseInCompanyAsync(long companyId, Address address);
        Task<bool> RemoveHouseInCompanyAsync(long companyId, long houseId);
        Task<bool> UpdateHouseInCompanyAsync(long companyId, long houseId, House newHouse);
        #endregion


        #region Banks

        #endregion
    }

    /// <summary>
    /// Доступ к материалам стен (СПРАВОЧНИК)
    /// </summary>
    public interface IWallMaterialRepository : IGenericDataRepository<WallMaterial>
    {
        void UpdateTest(string name);
    }
}