using KuaforRandevu2.Models.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KuaforRandevu2.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly KuaforWebContext dataContext;
        public DbSet<T> Table { get; set; }

        public Repository(KuaforWebContext dataContext)
        {
            this.dataContext = dataContext;
            Table = dataContext.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return Table;
        }
        public T Get(Expression<Func<T, bool>> expression)
        {
            return Table.FirstOrDefault(expression);
        }
        public T Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] expressions)
        {
            var query = Table.Where(expression);
            return expressions.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).FirstOrDefault();
        }


        public void Add(T entity)
        {
            Table.Add(entity);
        }
        public void Delete(T entity)
        {
            Table.Remove(entity);
        }
        public void Update(T entity)
        {
            Table.Update(entity);
        }
        public bool Save()
        {
            try
            {
                dataContext.SaveChanges();
                return true;
            }
            catch(DbUpdateConcurrencyException e)
            {
                return false;
            }
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return Table.Where(expression);
        }
        public IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc)
        {
            if (isDesc)
                return Table.OrderByDescending(orderBy);
            return Table.OrderBy(orderBy);
        }
    }
}
