using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors
{
    public abstract class AbstractService<T> : IService<T> where T : class
    {
        protected DatabaseContext _context;
        protected DbSet<T> _storage;

        protected AbstractService(DatabaseContext dbContext, DbSet<T> storge)
        {
            _context = dbContext;
            _storage = storge;
        }

        public abstract T Add(T entity);

        public T? Find(Expression<Func<T, bool>> finder)
        {
            return _storage.Find(finder);
        }

        public void Remove(T entity)
        {
            _storage.Remove(entity);
        }

        protected int FindFreeNumber(Expression<Func<T, int>> finder)
        {
            return Finder.FindFreeNumbers<T>(_storage, finder)[0];
        }
    }
}
