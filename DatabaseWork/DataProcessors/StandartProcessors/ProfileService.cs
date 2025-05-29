using System.Linq.Expressions;
using DatabaseWork.DataClasses;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors
{
    public class ProfileService : AbstractService<Profile>
    {
        public SpecialityService GroupService { get; set; }

        public ProfileService(DatabaseContext context) : base(context, context.Profiles)
        { }

        public override Profile Add(Profile entity)
        {
            int free_id = Finder.FindFreeNumbers<Profile>(_storage, sg => sg.IDpro)[0];
            entity.IDpro = free_id;

            return entity;
        }

        public Profile UpdateTitle(Profile entity, string title)
        {
            entity.ProTitle = title;
            return entity;
        }

        public Profile UpdateYear(Profile entity, int year)
        {
            entity.ProYear = year;
            return entity;
        }

        public Profile UpdateAdminissionYear(Profile entity, int adminission_year)
        {
            entity.ProAdminissionYear = adminission_year;
            return entity;
        }
    }
}
