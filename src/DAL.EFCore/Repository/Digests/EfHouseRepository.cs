using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Abstract.Concrete;
using DAL.Abstract.Entities.Digests.HouseDigests;
using DAL.EFCore.Entities.HouseDigests;

namespace DAL.EFCore.Repository.Digests
{
    public class EfHouseRepository : EfBaseRepository<EfHouse, HouseOld>, IHouseRepository
    {
        #region ctor

        public EfHouseRepository(string connectionString) : base(connectionString)
        {
        }

        #endregion



        #region CRUD

        public new HouseOld GetById(int id)
        {
            return base.GetById(id);
        }


        public new async Task<HouseOld> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }


        public new HouseOld GetSingle(Expression<Func<HouseOld, bool>> predicate)
        {
            return base.GetSingle(predicate);
        }


        public new async Task<HouseOld> GetSingleAsync(Expression<Func<HouseOld, bool>> predicate)
        {
            return await base.GetSingleAsync(predicate);
        }


        public new IEnumerable<HouseOld> GetWithInclude(params Expression<Func<HouseOld, object>>[] includeProperties)
        {
            return base.GetWithInclude(includeProperties);
        }


        public new IEnumerable<HouseOld> List()
        {
            return base.List();
        }


        public new IEnumerable<HouseOld> List(Expression<Func<HouseOld, bool>> predicate)
        {
            return base.List(predicate);
        }


        public new async Task<IEnumerable<HouseOld>> ListAsync()
        {
            return await base.ListAsync();
        }


        public new async Task<IEnumerable<HouseOld>> ListAsync(Expression<Func<HouseOld, bool>> predicate)
        {
            return await base.ListAsync(predicate);
        }


        public new int Count(Expression<Func<HouseOld, bool>> predicate)
        {
            return base.Count(predicate);
        }


        public new async Task<int> CountAsync(Expression<Func<HouseOld, bool>> predicate)
        {
            return await base.CountAsync(predicate);
        }


        public new void Add(HouseOld entity)
        {
            base.Add(entity);
        }


        public new async Task AddAsync(HouseOld entity)
        {
            await base.AddAsync(entity);
        }


        public new void AddRange(IEnumerable<HouseOld> entitys)
        {
            base.AddRange(entitys);
        }


        public new async Task AddRangeAsync(IEnumerable<HouseOld> entitys)
        {
            await base.AddRangeAsync(entitys);
        }


        public new void Delete(HouseOld entity)
        {
            base.Delete(entity);
        }


        public new void Delete(Expression<Func<HouseOld, bool>> predicate)
        {
            base.Delete(predicate);
        }


        public new async Task DeleteAsync(HouseOld entity)
        {
            await base.DeleteAsync(entity);
        }


        public new async Task DeleteAsync(Expression<Func<HouseOld, bool>> predicate)
        {
            await base.DeleteAsync(predicate);
        }


        public new void Edit(HouseOld entity)
        {
            base.Edit(entity);
        }


        public new async Task EditAsync(HouseOld entity)
        {
            await base.EditAsync(entity);
        }


        public new bool IsExist(Expression<Func<HouseOld, bool>> predicate)
        {
            return base.IsExist(predicate);
        }


        public new async Task<bool> IsExistAsync(Expression<Func<HouseOld, bool>> predicate)
        {
            return await base.IsExistAsync(predicate);
        }

        #endregion
    }
}