using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Abstract.Concrete;
using DAL.Abstract.Entities.Digests.HouseDigests;
using DAL.EFCore.Entities.HouseDigests;

namespace DAL.EFCore.Repository.Digests
{
    public class EfHouseRepository : EfBaseRepository<EfHouse, House>, IHouseRepository
    {
        #region ctor

        public EfHouseRepository(string connectionString) : base(connectionString)
        {
        }

        #endregion



        #region CRUD

        public new House GetById(int id)
        {
            return base.GetById(id);
        }


        public new async Task<House> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }


        public new House GetSingle(Expression<Func<House, bool>> predicate)
        {
            return base.GetSingle(predicate);
        }


        public new async Task<House> GetSingleAsync(Expression<Func<House, bool>> predicate)
        {
            return await base.GetSingleAsync(predicate);
        }


        public new IEnumerable<House> GetWithInclude(params Expression<Func<House, object>>[] includeProperties)
        {
            return base.GetWithInclude(includeProperties);
        }


        public new IEnumerable<House> List()
        {
            return base.List();
        }


        public new IEnumerable<House> List(Expression<Func<House, bool>> predicate)
        {
            return base.List(predicate);
        }


        public new async Task<IEnumerable<House>> ListAsync()
        {
            return await base.ListAsync();
        }


        public new async Task<IEnumerable<House>> ListAsync(Expression<Func<House, bool>> predicate)
        {
            return await base.ListAsync(predicate);
        }


        public new int Count(Expression<Func<House, bool>> predicate)
        {
            return base.Count(predicate);
        }


        public new async Task<int> CountAsync(Expression<Func<House, bool>> predicate)
        {
            return await base.CountAsync(predicate);
        }


        public new void Add(House entity)
        {
            base.Add(entity);
        }


        public new async Task AddAsync(House entity)
        {
            await base.AddAsync(entity);
        }


        public new void AddRange(IEnumerable<House> entitys)
        {
            base.AddRange(entitys);
        }


        public new async Task AddRangeAsync(IEnumerable<House> entitys)
        {
            await base.AddRangeAsync(entitys);
        }


        public new void Delete(House entity)
        {
            base.Delete(entity);
        }


        public new void Delete(Expression<Func<House, bool>> predicate)
        {
            base.Delete(predicate);
        }


        public new async Task DeleteAsync(House entity)
        {
            await base.DeleteAsync(entity);
        }


        public new async Task DeleteAsync(Expression<Func<House, bool>> predicate)
        {
            await base.DeleteAsync(predicate);
        }


        public new void Edit(House entity)
        {
            base.Edit(entity);
        }


        public new async Task EditAsync(House entity)
        {
            await base.EditAsync(entity);
        }


        public new bool IsExist(Expression<Func<House, bool>> predicate)
        {
            return base.IsExist(predicate);
        }


        public new async Task<bool> IsExistAsync(Expression<Func<House, bool>> predicate)
        {
            return await base.IsExistAsync(predicate);
        }

        #endregion
    }
}