using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Database.Abstract;
using Database.Abstract.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Database.EFCore
{
    /// <summary>
    /// Базовый тип репозитория для EntitiFramework
    /// Все Get методы возвращают не отслеживаемые сущности, без Include.
    /// Нет метода Upadate, т.к. сущность не отслеживается.
    /// </summary>
    /// <typeparam name="TDb">Тип в системе хранения</typeparam>
    /// <typeparam name="TMap">Тип в бизнесс логики</typeparam>
    public abstract class EfBaseRepository<TDb, TMap> : IDisposable
                                                        where TDb : BaseEntity
                                                        where TMap : class
    {
   
        #region field

        protected readonly DbContext DbContext;
        protected readonly DbSet<TDb> DbSet;
        protected readonly IMapper Mapper;

        #endregion




        #region ctor

        protected EfBaseRepository(DbContext dbContext, IMapper mapper)
        {
            Mapper = mapper;
            DbContext = dbContext;
            DbSet = dbContext.Set<TDb>();
        }

        #endregion



        #region CRUD

        public virtual TMap GetById(long id)
        {
            var efEntity = DbSet.AsNoTracking().FirstOrDefault(db => db.Id == id);
            var entity = Mapper.Map<TMap>(efEntity);
            return entity;
        }


        public virtual async Task<TMap> GetByIdAsync(long id)
        {
            var efEntity = await DbSet.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id);
            var entity = Mapper.Map<TMap>(efEntity);
            return entity;
        }


        public virtual TMap GetSingle(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = Mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            var efEntity = DbSet.AsNoTracking().SingleOrDefault(efPredicate);
            var entity = Mapper.Map<TMap>(efEntity);
            return entity;
        }


        public virtual async Task<TMap> GetSingleAsync(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = Mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            var efEntity = await DbSet.AsNoTracking().SingleOrDefaultAsync(efPredicate); //DEBUG AsNoTracking
            var entity = Mapper.Map<TMap>(efEntity);
            return entity;
        }

        //TODO: Отладить!!!!  using: (IEnumerable<Phone> phones = phoneRepo.GetWithInclude(p=>p.Company);)
        public virtual IEnumerable<TMap> GetWithInclude(params Expression<Func<TMap, object>>[] includeProperties)
        {
            var list = new List<Expression<Func<TDb, object>>>();
            foreach (var includeProperty in includeProperties)
            {
                var efIncludeProperty = Mapper.MapExpression<Expression<Func<TDb, object>>>(includeProperty);
                list.Add(efIncludeProperty);
            }
            var result = Include(list.ToArray()).ToList();
            return Mapper.Map<IEnumerable<TMap>>(result);
        }


        public virtual IEnumerable<TMap> List()
        {
            var efEntitys = DbSet.AsNoTracking().ToList();
            var entity = Mapper.Map<IEnumerable<TMap>>(efEntitys);
            return entity;
        }


        public virtual IEnumerable<TMap> List(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = Mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            var efEntitys = DbSet.AsNoTracking().Where(efPredicate).ToList();
            var entity = Mapper.Map<IEnumerable<TMap>>(efEntitys);
            return entity;
        }


        public virtual async Task<IEnumerable<TMap>> ListAsync()
        {
            var efEntitys = await DbSet.AsNoTracking().ToListAsync();
            var entity = Mapper.Map<IEnumerable<TMap>>(efEntitys);
            return entity;
        }


        public virtual async Task<IEnumerable<TMap>> ListAsync(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = Mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            var efEntitys = await DbSet.AsNoTracking().Where(efPredicate).ToListAsync();
            var entity = Mapper.Map<IEnumerable<TMap>>(efEntitys);
            return entity;
        }


        public virtual int Count(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = Mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            return DbSet.Count(efPredicate);
        }


        public virtual async Task<int> CountAsync(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = Mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            return await DbSet.CountAsync(efPredicate);
        }


        public virtual void Add(TMap entity)
        {
            var efEntitys = Mapper.Map<TDb>(entity);
            DbSet.Add(efEntitys);
        }


        public virtual async Task AddAsync(TMap entity)
        {
            var efEntitys = Mapper.Map<TDb>(entity);
            await DbSet.AddAsync(efEntitys);
        }


        public virtual void AddRange(IEnumerable<TMap> entitys)
        {
            var efEntitys = Mapper.Map<IEnumerable<TDb>>(entitys);
            DbSet.AddRange(efEntitys);
        }


        public virtual async Task AddRangeAsync(IEnumerable<TMap> entitys)
        {
            var efEntitys = Mapper.Map<IEnumerable<TDb>>(entitys);
            await DbSet.AddRangeAsync(efEntitys);
        }


        public virtual void Delete(long id)
        {
            var efEntity = DbSet.Find(id);
            DbSet.Remove(efEntity);
        }


        public virtual async Task DeleteAsync(long id)
        {
            var efEntity = await DbSet.FindAsync(id);
            DbSet.Remove(efEntity);
        }


        public virtual void Delete(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = Mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            var efEntitys = DbSet.Where(efPredicate).ToList();
            DbSet.RemoveRange(efEntitys);
        }


        public virtual async Task DeleteAsync(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = Mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            var efEntitys = await DbSet.Where(efPredicate).ToListAsync();
            DbSet.RemoveRange(efEntitys);
        }




        public virtual bool IsExist(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = Mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            return DbSet.Any(efPredicate);
        }


        public virtual async Task<bool> IsExistAsync(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = Mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            return await DbSet.AnyAsync(efPredicate);
        }

        #endregion




        #region Methode

        private IQueryable<TDb> Include(params Expression<Func<TDb, object>>[] includeProperties)
        {
            IQueryable<TDb> query = DbSet.AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        #endregion




        #region Disposable

        public void Dispose()
        {
            DbContext?.Dispose();
        }

        #endregion
    }
}