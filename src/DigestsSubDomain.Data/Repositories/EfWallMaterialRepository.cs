using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Database.EFCore;
using Digests.Core.Model.House;
using Digests.Data.Abstract;
using Digests.Data.EfCore.DbContext;
using Digests.Data.EfCore.Entities.HouseDigests;
using Digests.Data.EfCore.Mapper;

namespace Digests.Data.EfCore.Repositories
{
    public class EfWallMaterialRepository : EfBaseRepository<EfWallMaterial, WallMaterial>, IWallMaterialRepository
    {
        #region ctor

        public EfWallMaterialRepository(Context context) : base(context, AutoMapperConfig.Mapper)
        {
        }

        #endregion



        #region CRUD

        public new WallMaterial GetById(int id)
        {
            return base.GetById(id);
        }


        public new async Task<WallMaterial> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }


        public new WallMaterial GetSingle(Expression<Func<WallMaterial, bool>> predicate)
        {
            return base.GetSingle(predicate);
        }


        public new async Task<WallMaterial> GetSingleAsync(Expression<Func<WallMaterial, bool>> predicate)
        {
            return await base.GetSingleAsync(predicate);
        }


        public new IEnumerable<WallMaterial> GetWithInclude(params Expression<Func<WallMaterial, object>>[] includeProperties)
        {
            return base.GetWithInclude(includeProperties);
        }


        public new IEnumerable<WallMaterial> List()
        {
            return base.List();
        }


        public new IEnumerable<WallMaterial> List(Expression<Func<WallMaterial, bool>> predicate)
        {
            return base.List(predicate);
        }


        public new async Task<IEnumerable<WallMaterial>> ListAsync()
        {
            return await base.ListAsync();
        }


        public new async Task<IEnumerable<WallMaterial>> ListAsync(Expression<Func<WallMaterial, bool>> predicate)
        {
            return await base.ListAsync(predicate);
        }


        public new int Count(Expression<Func<WallMaterial, bool>> predicate)
        {
            return base.Count(predicate);
        }


        public new async Task<int> CountAsync(Expression<Func<WallMaterial, bool>> predicate)
        {
            return await base.CountAsync(predicate);
        }


        public new void Add(WallMaterial entity)
        {
            base.Add(entity);
        }


        public new async Task AddAsync(WallMaterial entity)
        {
            await base.AddAsync(entity);
        }


        public new void AddRange(IEnumerable<WallMaterial> entitys)
        {
            base.AddRange(entitys);
        }


        public new async Task AddRangeAsync(IEnumerable<WallMaterial> entitys)
        {
            await base.AddRangeAsync(entitys);
        }


        public new void Delete(WallMaterial entity)
        {
            base.Delete(entity);
        }


        public new void Delete(Expression<Func<WallMaterial, bool>> predicate)
        {
            base.Delete(predicate);
        }


        public new async Task DeleteAsync(WallMaterial entity)
        {
            await base.DeleteAsync(entity);
        }


        public new async Task DeleteAsync(Expression<Func<WallMaterial, bool>> predicate)
        {
            await base.DeleteAsync(predicate);
        }


        public new void Edit(WallMaterial entity)
        {
            base.Edit(entity);
        }


        public new async Task EditAsync(WallMaterial entity)
        {
            await base.EditAsync(entity);
        }


        public new bool IsExist(Expression<Func<WallMaterial, bool>> predicate)
        {
            return base.IsExist(predicate);
        }


        public new async Task<bool> IsExistAsync(Expression<Func<WallMaterial, bool>> predicate)
        {
            return await base.IsExistAsync(predicate);
        }

        #endregion
    }
}