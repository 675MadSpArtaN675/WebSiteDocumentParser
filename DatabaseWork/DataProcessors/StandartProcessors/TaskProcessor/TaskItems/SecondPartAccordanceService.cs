using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors.TaskProcessor.TaskItems
{
    public class SecondPartAccordanceService : AbstractService<SecondPartAccordance>
    {
        public SecondPartAccordanceService(DatabaseContext context) : base(context, context.SecondPartsAccordances)
        { }

        public override SecondPartAccordance Add(SecondPartAccordance entity)
        {
            int free_id = FindFreeNumber(ia => ia.IDspa);
            entity.IDspa = free_id;

            _storage.Add(entity);

            return entity;
        }

        public SecondPartAccordance UpdateLinks(SecondPartAccordance entity, Task_d? task = null)
        {
            if (task != null)
                entity.TaskLink = task;

            _storage.Update(entity);

            return entity;
        }
    }
}

