using System.Linq.Expressions;
using DatabaseWork.DataClasses;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors
{
    public class SpecialityService : AbstractService<Speciality>
    {
        public LevelService LevelService { get; protected set; }
        public SpecGroupService SpecGroupService { get; protected set; }

        public SpecialityService(DatabaseContext context, LevelService levelService, SpecGroupService specGroupService) : base(context, context.Specialities)
        {
            LevelService = levelService;
            SpecGroupService = specGroupService;
        }

        public override Speciality Add(Speciality entity)
        {
            int free_id = Finder.FindFreeNumbers<Speciality>(_context.Specialities, sp => sp.IDspec)[0];
            entity.IDspec = free_id;

            _storage.Add(entity);

            return entity;
        }

        public Speciality UpdateLinkFull(Speciality entity, Level? level = null, SpecGroup? specGroup = null)
        {
            if (level != null)
                entity.EdLevel = level;

            if (specGroup != null)
                entity.SGroup = specGroup;

            return entity;
        }

        public Speciality? UpdateTitle(Speciality entity, string title)
        {
            entity.SpecTitle = title;

            return entity;
        }

        public Speciality? UpdateNumber(Speciality entity, string number)
        {
            entity.SpecNumber = number;

            return entity;
        }
    }
}
