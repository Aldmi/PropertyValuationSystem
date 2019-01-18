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
    /// </summary>
    /// <typeparam name="TDb">Тип в системе хранения</typeparam>
    /// <typeparam name="TMap">Тип в бизнесс логики</typeparam>
    public abstract class EfBaseRepository<TDb, TMap> : IDisposable
                                                        where TDb : class, IEntity
                                                        where TMap : class
    {
   
        #region field

        protected readonly DbContext DbContext;
        protected readonly DbSet<TDb> DbSet;
        private readonly IMapper _mapper;

        #endregion




        #region ctor

        protected EfBaseRepository(DbContext dbDbContext, IMapper mapper)
        {
            _mapper = mapper;
            DbContext = dbDbContext;
            DbSet = DbContext.Set<TDb>();
        }

        #endregion



        #region CRUD

        protected TMap GetById(int id)
        {
            var efSpOption = DbSet.Find(id);
            var spOptions = _mapper.Map<TMap>(efSpOption);
            return spOptions;
        }


        protected async Task<TMap> GetByIdAsync(int id)
        {
            var efSpOption = await DbSet.FindAsync(id);
            var spOptions = _mapper.Map<TMap>(efSpOption);
            return spOptions;
        }


        protected TMap GetSingle(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = _mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            var efSpOption = DbSet.SingleOrDefault(efPredicate);
            var spOption = _mapper.Map<TMap>(efSpOption);
            return spOption;
        }


        protected async Task<TMap> GetSingleAsync(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = _mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            var efSpOption = await DbSet.SingleOrDefaultAsync(efPredicate);
            var spOption = _mapper.Map<TMap>(efSpOption);
            return spOption;
        }

        //TODO: Отладить!!!!  using: (IEnumerable<Phone> phones = phoneRepo.GetWithInclude(p=>p.Company);)
        public IEnumerable<TMap> GetWithInclude(params Expression<Func<TMap, object>>[] includeProperties)
        {
            var list = new List<Expression<Func<TDb, object>>>();
            foreach (var includeProperty in includeProperties)
            {
                var efIncludeProperty = _mapper.MapExpression<Expression<Func<TDb, object>>>(includeProperty);
                list.Add(efIncludeProperty);
            }
            var result = Include(list.ToArray()).ToList();
            return _mapper.Map<IEnumerable<TMap>>(result);
        }


        protected IEnumerable<TMap> List()
        {
            var efOptions = DbSet.ToList();
            var spOptions = _mapper.Map<IEnumerable<TMap>>(efOptions);
            return spOptions;
        }


        protected IEnumerable<TMap> List(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = _mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            var efOptions = DbSet.Where(efPredicate).ToList();
            var spOptions = _mapper.Map<IEnumerable<TMap>>(efOptions);
            return spOptions;
        }


        protected async Task<IEnumerable<TMap>> ListAsync()
        {
            var efOptions = await DbSet.ToListAsync();
            var spOptions = _mapper.Map<IEnumerable<TMap>>(efOptions);
            return spOptions;
        }


        protected async Task<IEnumerable<TMap>> ListAsync(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = _mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            var efOptions = await DbSet.Where(efPredicate).ToListAsync();
            var spOptions = _mapper.Map<IEnumerable<TMap>>(efOptions);
            return spOptions;
        }


        protected int Count(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = _mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            return DbSet.Count(efPredicate);
        }


        protected async Task<int> CountAsync(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = _mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            return await DbSet.CountAsync(efPredicate);
        }


        protected void Add(TMap entity)
        {
            var efOptions = _mapper.Map<TDb>(entity);
            DbSet.Add(efOptions);
            DbContext.SaveChanges();
        }


        protected async Task AddAsync(TMap entity)
        {
            var efOptions = _mapper.Map<TDb>(entity);
            await DbSet.AddAsync(efOptions);
            await DbContext.SaveChangesAsync();
        }


        protected void AddRange(IEnumerable<TMap> entitys)
        {
            var efOptions = _mapper.Map<IEnumerable<TDb>>(entitys);
            DbSet.AddRange(efOptions);
            DbContext.SaveChanges();
        }


        protected async Task AddRangeAsync(IEnumerable<TMap> entitys)
        {
            //DEBUG
            try
            {
                var efOptions = _mapper.Map<IEnumerable<TDb>>(entitys);
                await DbSet.AddRangeAsync(efOptions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        
        
            await DbContext.SaveChangesAsync();
        }


        protected void Delete(TMap entity)
        {
            var efOptions = _mapper.Map<TDb>(entity);
            DbSet.Remove(efOptions);
            DbContext.SaveChanges();
        }


        protected void Delete(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = _mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            var efOptions = DbSet.Where(efPredicate).ToList();
            DbSet.RemoveRange(efOptions);
            DbContext.SaveChanges();
        }


        protected async Task DeleteAsync(TMap entity)
        {
            var efOptions = _mapper.Map<TDb>(entity);
            DbSet.Remove(efOptions);
            await DbContext.SaveChangesAsync();
        }


        protected async Task DeleteAsync(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = _mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            var efOptions = await DbSet.Where(efPredicate).ToListAsync();
            DbSet.RemoveRange(efOptions);
            await DbContext.SaveChangesAsync();
        }


        protected void Edit(TMap entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }


        protected async Task EditAsync(TMap entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }


        protected bool IsExist(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = _mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
            return DbSet.Any(efPredicate);
        }


        protected async Task<bool> IsExistAsync(Expression<Func<TMap, bool>> predicate)
        {
            var efPredicate = _mapper.MapExpression<Expression<Func<TDb, bool>>>(predicate);
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