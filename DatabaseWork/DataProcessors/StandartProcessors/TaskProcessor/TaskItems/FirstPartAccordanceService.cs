using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors.TaskProcessor.TaskItems
{
    public class FirstPartAccordanceService : AbstractService<FirstPartAccordance>
    {
        public FirstPartAccordanceService(DatabaseContext context) : base(context, context.FirstPartsAccordances)
        { }

        public override FirstPartAccordance Add(FirstPartAccordance entity)
        {
            int free_id = FindFreeNumber(ia => ia.IDfpa);
            entity.IDfpa = free_id;

            return entity;
        }
    }
}

