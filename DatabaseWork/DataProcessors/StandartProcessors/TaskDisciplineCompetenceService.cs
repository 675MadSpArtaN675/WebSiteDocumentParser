using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors
{
    public class TaskDisciplineCompetenceService : AbstractService<TaskDesciplineCompetenceLink>
    {
        public TaskDisciplineCompetenceService(DatabaseContext context) : base(context, context.FullTDC)
        { }

        public override TaskDesciplineCompetenceLink Add(TaskDesciplineCompetenceLink entity)
        {
            entity.IDtdc = FindFreeNumber(e => e.IDtdc);
            _storage.Add(entity);

            return entity;
        }

        public TaskDesciplineCompetenceLink UpdateLinks(TaskDesciplineCompetenceLink entity, Task_d? task = null, DisciplineCompetenceLink? dcl = null)
        {
            if (task != null)
                entity.TaskLink = task;

            if (dcl != null)
                entity.FullDCLink = dcl;

            _storage.Update(entity);

            return entity;
        }
    }
}
