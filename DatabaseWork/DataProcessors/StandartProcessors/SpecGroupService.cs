using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DatabaseWork.DataClasses;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors
{
    public class SpecGroupService : AbstractService<SpecGroup>
    {
        public SpecGroupService(DatabaseContext context) : base(context, context.SpecGroups)
        { }

        public override SpecGroup Add(SpecGroup entity)
        {
            int free_id = Finder.FindFreeNumbers<SpecGroup>(_context.SpecGroups, sg => sg.IDsg)[0];
            entity.IDsg = free_id;

            _storage.Add(entity);

            return entity;
        }
        public SpecGroup? UpdateNumber(SpecGroup result, string new_number)
        {
            result.SGNumber = new_number;

            return result;
        }

        public SpecGroup? UpdateTitle(SpecGroup result, string new_title)
        {
            result.SGTitle = new_title;

            return result;
        }
    }
}
