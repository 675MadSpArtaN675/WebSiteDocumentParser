using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;
using Microsoft.EntityFrameworkCore;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors.TaskProcessor.TaskItems
{
    public class ItemsAccordanceService : AbstractService<ItemsAccordance>
    {
        public ItemsAccordanceService(DatabaseContext context) : base(context, context.ItemsAccordance)
        { }

        public override ItemsAccordance Add(ItemsAccordance entity)
        {
            entity.IDia = FindFreeNumber(ia => ia.IDia);

            _storage.Add(entity);

            return entity;
        }

        public ItemsAccordance UpdateLinks(ItemsAccordance entity, Task_d? task = null)
        {
            if (task != null)
                entity.TaskLink = task;

            _storage.Update(entity);

            return entity;
        }
    }
}

