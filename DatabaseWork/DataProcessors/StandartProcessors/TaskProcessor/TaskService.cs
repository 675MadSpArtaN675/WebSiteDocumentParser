using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors.TaskProcessor
{
    public class TaskService : AbstractService<Task_d>
    {
        public TypeTaskService typeTaskService { get; set; }

        public TaskService(DatabaseContext context, TypeTaskService typeTasks) : base(context, context.Tasks)
        {
            typeTaskService = typeTasks;
        }

        public override Task_d Add(Task_d entity)
        {
            int free_id = FindFreeNumber(t => t.IDtask);
            _storage.Add(entity);

            return entity;
        }

        public Task_d UpdateLinks(Task_d entity, TypeTask? typeTasks = null)
        {
            if (typeTasks != null)
                entity.TaskType = typeTasks;

            return entity;
        }
    }
}
