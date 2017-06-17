using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HomeWork1.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IUnitOfWork UnitOfWork { get;set; }

        private DbSet<T> _Objectset;

        private DbSet<T> ObjectSet
        {
            get
            {
                if (_Objectset == null)
                {
                    _Objectset = UnitOfWork.Context.Set<T>();
                }
                return _Objectset;
            }
        }

        public Repository(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public void Commit()
        {
            this.UnitOfWork.Save();
        }

        public void Create(T entity)
        {
            this.ObjectSet.Add(entity);
        }

        public T GetSingle(Expression<Func<T, bool>> filter)
        {
            return ObjectSet.SingleOrDefault(filter);
        }

        public IQueryable<T> LookupAll()
        {
            return this.ObjectSet;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter)
        {
            return this.ObjectSet.Where(filter);
        }

        public void Remove(T entity)
        {
            ObjectSet.Remove(entity);
        }
    }
}