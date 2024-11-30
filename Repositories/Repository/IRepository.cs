using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetByConditionAsync(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate);

        Task<T> GetWithIncludesAndFiltersAsync(List<Expression<Func<T, bool>>> filters, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetWithIncludesAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
     }
}
