using System.Data.Entity;
using HomeWork1.Models;

namespace HomeWork1.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }


        public EFUnitOfWork()
        {
            Context = new SkillTreeHomeworkEntities();
        }


        public void Dispose()
        {
            Context.Dispose();
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}