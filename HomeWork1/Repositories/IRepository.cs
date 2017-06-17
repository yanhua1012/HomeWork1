using System;
using System.Linq;
using System.Linq.Expressions;

namespace HomeWork1.Repositories
{
    public interface IRepository<T> where T:class
    {
        IUnitOfWork UnitOfWork { get; set; }

        IQueryable<T> LookupAll();

        IQueryable<T> Query(Expression<Func<T, bool>> filter);

        T GetSingle(Expression<Func<T, bool>> filter);

        void Create(T entity);

        void Remove(T entity);

        void Commit();
    }
}
