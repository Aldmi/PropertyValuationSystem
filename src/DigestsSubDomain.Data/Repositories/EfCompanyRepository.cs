using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Database.EFCore;
using Digests.Core.Model;
using Digests.Data.Abstract;
using Digests.Data.EfCore.DbContext;
using Digests.Data.EfCore.Entities;
using Digests.Data.EfCore.Mapper;

namespace Digests.Data.EfCore.Repositories
{
    public class EfCompanyRepository : EfBaseRepository<EfCompany, Company>, ICompanyRepository
    {
        #region ctor

        public EfCompanyRepository(Context context) : base(context, AutoMapperConfig.Mapper)
        {
        }

        #endregion



        #region CRUD

        public new Company GetById(int id)
        {
            return base.GetById(id);
        }


        public new async Task<Company> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }


        public new Company GetSingle(Expression<Func<Company, bool>> predicate)
        {
            return base.GetSingle(predicate);
        }


        public new async Task<Company> GetSingleAsync(Expression<Func<Company, bool>> predicate)
        {
            return await base.GetSingleAsync(predicate);
        }


        public new IEnumerable<Company> GetWithInclude(params Expression<Func<Company, object>>[] includeProperties)
        {
            return base.GetWithInclude(includeProperties);
        }


        public new IEnumerable<Company> List()
        {
            return base.List();
        }


        public new IEnumerable<Company> List(Expression<Func<Company, bool>> predicate)
        {
            return base.List(predicate);
        }


        public new async Task<IEnumerable<Company>> ListAsync()
        {
            return await base.ListAsync();
        }


        public new async Task<IEnumerable<Company>> ListAsync(Expression<Func<Company, bool>> predicate)
        {
            return await base.ListAsync(predicate);
        }


        public new int Count(Expression<Func<Company, bool>> predicate)
        {
            return base.Count(predicate);
        }


        public new async Task<int> CountAsync(Expression<Func<Company, bool>> predicate)
        {
            return await base.CountAsync(predicate);
        }


        public new void Add(Company entity)
        {
            base.Add(entity);
        }


        public new async Task AddAsync(Company entity)
        {
            await base.AddAsync(entity);
        }


        public new void AddRange(IEnumerable<Company> entitys)
        {
            base.AddRange(entitys);
        }


        public new async Task AddRangeAsync(IEnumerable<Company> entitys)
        {
            await base.AddRangeAsync(entitys);
        }


        public new void Delete(Company entity)
        {
            base.Delete(entity);
        }


        public new void Delete(Expression<Func<Company, bool>> predicate)
        {
            base.Delete(predicate);
        }


        public new async Task DeleteAsync(Company entity)
        {
            await base.DeleteAsync(entity);
        }


        public new async Task DeleteAsync(Expression<Func<Company, bool>> predicate)
        {
            await base.DeleteAsync(predicate);
        }


        public new void Edit(Company entity)
        {
            base.Edit(entity);
        }


        public new async Task EditAsync(Company entity)
        {
            await base.EditAsync(entity);
        }


        public new bool IsExist(Expression<Func<Company, bool>> predicate)
        {
            return base.IsExist(predicate);
        }


        public new async Task<bool> IsExistAsync(Expression<Func<Company, bool>> predicate)
        {
            return await base.IsExistAsync(predicate);
        }

        #endregion
    }
}