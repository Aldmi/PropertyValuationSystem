using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shared.Kernel.ForDomain;

namespace Database.Abstract.Abstract
{
    /// <summary>
    /// Абстрактный репозиторий.
    /// </summary>
    /// <typeparam name="T">Доступ ТОЛЬКО через КОРЕНЬ АГРЕГАЦИИ</typeparam>
    public interface IGenericDataRepository<T> where T : DomainAggregateRoot
    {
        T GetById(long id);
        Task<T> GetByIdAsync(long id);

        T GetSingle(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties); //?????

        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> ListAsync();
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate);

        int Count(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        Task AddAsync(T entity);

        void AddRange(IEnumerable<T> entitys); 
        Task AddRangeAsync(IEnumerable<T> entitys); 

        void Delete(long id);
        void Delete(Expression<Func<T, bool>> predicate);
        Task DeleteAsync(long id);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);

        bool IsExist(Expression<Func<T, bool>> predicate);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate);
    }
}