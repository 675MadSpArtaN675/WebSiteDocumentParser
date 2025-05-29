using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors.TaskProcessor
{
    public class TypeTaskService : AbstractService<TypeTask>
    {
        public TypeTaskService(DatabaseContext context) : base(context, context.TaskTypes)
        { }

        public override TypeTask Add(TypeTask entity)
        {
            int id = FindFreeNumber(e => e.Idtt);
            entity.Idtt = id;

            _storage.Add(entity);

            return entity;
        }
    }
}
