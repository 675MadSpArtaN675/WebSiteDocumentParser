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

            return entity;
        }

        public SecondPartAccordance Add(SecondPartAccordance entity)
        {
            entity.IDspa = FindFreeNumber(ia => ia.IDia);

            return entity;
        }
    }
}

