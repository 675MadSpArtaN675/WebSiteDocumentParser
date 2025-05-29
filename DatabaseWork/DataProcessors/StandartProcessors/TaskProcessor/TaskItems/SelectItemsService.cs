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

            _storage.Add(entity);

            return entity;
        }

        public SelectedItems UpdateLinks(SelectedItems entity, Task_d? task = null)
        {
            if (task != null)
                entity.TaskLink = task;

            _storage.Update(entity);

            return entity;
        }
    }
}
