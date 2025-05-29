using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors.TaskProcessor.TaskItems
{
    public class SelectItemsService : AbstractService<SelectedItems>
    {
        public SelectItemsService(DatabaseContext context) : base(context, context.SelectedItems)
        { }

        public override SelectedItems Add(SelectedItems entity)
        {
            entity.IDSelect = FindFreeNumber(si => entity.IDSelect);

            return entity;
        }
    }
}
