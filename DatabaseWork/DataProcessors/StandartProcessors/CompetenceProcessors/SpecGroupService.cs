using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DatabaseWork.DataClasses;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;
using DatabaseWork.DataProcessors.StandartProcessors.TechnicClasses;
using DocumentFormat.OpenXml.Vml.Office;

namespace DatabaseWork.DataProcessors.StandartProcessors.CompetenceProcessors
{
    public class SpecGroupService : AbstractService<SpecGroup>
    {
        public SpecGroupService(DatabaseContext context) : base(context, context.SpecGroups)
        { }

        public override SpecGroup Add(SpecGroup entity)
        {
            int free_id = Finder.FindFreeNumbers(_context.SpecGroups, sg => sg.IDsg)[0];
            entity.IDsg = free_id;

            _storage.Add(entity);

            return entity;
        }
        public SpecGroup? UpdateNumber(SpecGroup entity, string new_number)
        {
            entity.SGNumber = new_number;
            _storage.Update(entity);

            return entity;
        }

        public SpecGroup? UpdateTitle(SpecGroup entity, string new_title)
        {
            entity.SGTitle = new_title;
            _storage.Update(entity);

            return entity;
        }
    }
}
