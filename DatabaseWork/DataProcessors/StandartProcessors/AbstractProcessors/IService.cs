using System.Linq.Expressions;

namespace DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors
{
    public interface IService<T>
    {
        T Add(T entity);
        void Remove(T entity);
        T? Find(Expression<Func<T, bool>> finder);
    }
}
