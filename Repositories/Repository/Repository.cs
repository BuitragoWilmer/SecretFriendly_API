using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SecretSanta_DBContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(SecretSanta_DBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }


        public async Task<T> GetWithIncludesAndFiltersAsync(
    List<Expression<Func<T, bool>>> filters,
    params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsQueryable();


            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            Expression<Func<T, bool>> combinedFilter = x => true;

            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    combinedFilter = CombineFilters(combinedFilter, filter);
                }
            }


            query = query.Where(combinedFilter);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetWithIncludesAsync(
            Expression<Func<T, bool>> filter,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            // Aplicar los includes
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            // Aplicar el filtro y retornar el resultado
            return await query.FirstOrDefaultAsync(filter);
        }

       
        private Expression<Func<T, bool>> CombineFilters(Expression<Func<T, bool>> firstFilter, Expression<Func<T, bool>> secondFilter)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var body = Expression.AndAlso(
                Expression.Invoke(firstFilter, parameter),
                Expression.Invoke(secondFilter, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }


        public async Task<IEnumerable<T>> GetByConditionAsync(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
