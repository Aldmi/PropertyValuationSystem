using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Database.EFCore;
using Digests.Core.Model._4Company;
using Digests.Core.Model._4House;
using Digests.Data.Abstract;
using Digests.Data.EfCore.DbContext;
using Digests.Data.EfCore.Entities._4Company;
using Digests.Data.EfCore.Entities._4House;
using Digests.Data.EfCore.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Digests.Data.EfCore.Repositories
{
    public class EfCompanyRepository : EfBaseRepository<EfCompany, Company>, ICompanyRepository
    {
        #region fields

        private readonly Context _context;

        #endregion


        #region ctor

        public EfCompanyRepository(Context context) : base(context, AutoMapperConfig.Mapper)
        {
            _context = context;
        }

        #endregion



        #region Special Methods
        public async Task<Company> GetCompanyByNameAsync(string companyName)
        {
            var efCompany = await _context.Companys.AsNoTracking()
                .Include(c => c.Houses).
                FirstOrDefaultAsync(c => c.Name == companyName);
            var company = Mapper.Map<Company>(efCompany);
            return company;
        }


        public async Task<IReadOnlyList<House>> GetAllHouseAsync(long companyId)
        {
          var housesEf= await _context.Houses.AsNoTracking()
              .Include(h=> h.WallMaterial)
              .Where(h => h.EfCompanyId == companyId)
              .ToListAsync();
          var houses = Mapper.Map<IReadOnlyList<House>>(housesEf);
          return houses;
        }


        public async Task<House> GetHouseAsync(long companyId, long houseId)
        {
           var efHouse= await _context.Houses.AsNoTracking()
               .Include(h => h.WallMaterial)
               .FirstOrDefaultAsync(h => h.EfCompanyId == companyId && h.Id == houseId);
           var house = Mapper.Map<House>(efHouse);
           return house;
        }


        public async Task<House> GetHouseAsync(long companyId, Address address)
        {
           var efHouse = await _context.Houses.AsNoTracking()
               .Include(h => h.WallMaterial)
               .FirstOrDefaultAsync(h => h.EfCompanyId == companyId );//&& h.Address == address
           var house = Mapper.Map<House>(efHouse);
           return house;
        }


        /// <summary>
        /// Вернуть все дома фирмы из заданного материала стен 
        /// </summary>
        public async Task<IReadOnlyList<WallMaterial>> GetAllWallMaterialsAsync(long companyId)
        {
            var wallmaterialsEf = await _context.Houses.AsNoTracking()
                .Where(h => h.EfCompanyId == companyId)
                .Select(h=>h.WallMaterial)
                .Where(w=> w.IsShared == false)
                .ToListAsync();
            var wallmaterials = Mapper.Map<IReadOnlyList<WallMaterial>>(wallmaterialsEf);
            return wallmaterials;
        }


        /// <summary>
        /// Вернуть все дома фирмы из заданного материала стен 
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="nameWallmaterial"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<House>> GetHouseOfWallMaterialAsync(long companyId, string nameWallmaterial)
        {
            var efHouses = await _context.Houses.AsNoTracking()
                .Include(h => h.WallMaterial)
                .Where(h => h.EfCompanyId == companyId && h.WallMaterial.Name == nameWallmaterial)
                .ToListAsync();
            var houses = Mapper.Map<IReadOnlyList<House>>(efHouses);
            return houses;
        }


        public async Task<bool> UpdateCompanyDetailsAsync(long companyId, CompanyDetails newCompanyDetails)
        {
            var efCompany = await _context.Companys.FindAsync(companyId);
            if(efCompany == null)
              throw new KeyNotFoundException("Компания не найденна");

            efCompany.CompanyDetails = newCompanyDetails;
            var res= _context.Companys.Update(efCompany);
            return res.State == EntityState.Modified;
        }


        public async Task<bool> AddHouseInCompanyAsync(long companyId, House house)
        {
            var efHouse = Mapper.Map<EfHouse>(house);
            efHouse.EfCompanyId = companyId;
            var res= await _context.Houses.AddAsync(efHouse);
            return res.State == EntityState.Added;
        }


        public async Task<bool> RemoveHouseInCompanyAsync(long companyId, Address address)
        {
            var efHouse = await _context.Houses.FirstOrDefaultAsync(h => h.EfCompanyId == companyId && h.Address == address);
            if (efHouse == null)
                throw new KeyNotFoundException("Компания или дом не найденны");

            var res= _context.Houses.Remove(efHouse);
            return res.State == EntityState.Deleted;
        }


        public async Task<bool> RemoveHouseInCompanyAsync(long companyId, long houseId)
        {
            var efHouse = await _context.Houses.FirstOrDefaultAsync(h => h.EfCompanyId == companyId && h.Id == houseId);
            if (efHouse == null)
                throw new KeyNotFoundException("Компания или дом не найденны");

            var res = _context.Houses.Remove(efHouse);
            return res.State == EntityState.Deleted;
        }


        public async Task<bool> UpdateHouseInCompanyAsync(long companyId, long houseId, House newHouse)
        {
            var efHouse= await _context.Houses.FirstOrDefaultAsync(h => h.EfCompanyId == companyId && h.Id == houseId);
            if (efHouse == null)
                throw new KeyNotFoundException("Дом не найден");

            efHouse.Address = newHouse.Address;
            efHouse.MetroStation = newHouse.MetroStation;
            efHouse.Year = newHouse.Year;
            if (newHouse.WallMaterial == null)  // материал стен не известен
            {
                efHouse.WallMaterialId = null;
            }
            else
            if(newHouse.WallMaterial.Id != 0)   // материал стен уже добавленный
            {
                efHouse.WallMaterialId = newHouse.WallMaterial.Id;
            }
            else
            if (newHouse.WallMaterial.Id == 0)  // материал стен новый
            {
                var efWallMaterial= Mapper.Map<EfWallMaterial>(newHouse.WallMaterial);
                efHouse.WallMaterial = efWallMaterial;
            }

            var res = _context.Houses.Update(efHouse);
            return res.State == EntityState.Modified;
        }
    }

    #endregion


}