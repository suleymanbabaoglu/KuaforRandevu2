using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;


namespace KuaforRandevu2.Repository
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }

        IQueryable<T> Get();
        T Get(Expression<Func<T, bool>> expression);
        T Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] expressions);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Save();

        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> expression, bool isDesc);
    }
}
