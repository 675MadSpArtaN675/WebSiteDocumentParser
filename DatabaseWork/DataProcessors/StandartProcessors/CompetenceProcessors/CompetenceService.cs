using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseWork.DataClasses;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors.CompetenceProcessors
{
    public class CompetenceService : AbstractService<Competence>
    {
        public ProfileService ProfileService { get; protected set; }
        public TypeCompetenceService TypeCompetenceService { get; protected set; }

        public CompetenceService(DatabaseContext context, ProfileService profileService, TypeCompetenceService typeCompetenceService) : base(context, context.Competences)
        {
            ProfileService = profileService;
            TypeCompetenceService = typeCompetenceService;
        }

        public override Competence Add(Competence entity)
        {
            int free_id = FindFreeNumber(c => c.IDcomp);
            entity.IDcomp = free_id;

            _storage.Add(entity);
            return entity;
        }

        public Competence UpdateLinks(Competence entity, Profile? profile = null, TypeCompetence? typeCompetence = null)
        {
            if (profile != null)
                entity.ProfileLink = profile;

            if (typeCompetence != null)
                entity.CompType = typeCompetence;

            return entity;
        }

        public Competence UpdateTitle(Competence entity, string title)
        {
            if (title != null)
                entity.CompNumber = title;

            return entity;
        }
    }
}
