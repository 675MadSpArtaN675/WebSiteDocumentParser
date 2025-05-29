using DatabaseWork.DataClasses;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors.CompetenceProcessors
{
    public class DisciplineCompetenceLinkService : AbstractService<DisciplineCompetenceLink>
    {

        public DisciplineCompetenceLinkService(DatabaseContext context) : base(context, context.FullDiscipline)
        { }

        public override DisciplineCompetenceLink Add(DisciplineCompetenceLink entity)
        {
            entity.IDdc = FindFreeNumber(e => e.IDdc);
            _storage.Add(entity);

            return entity;
        }

        public DisciplineCompetenceLink UpdateLinks(DisciplineCompetenceLink dc_link, Competence? comp = null, Discipline? discipline = null)
        {
            if (comp != null)
                dc_link.CompetenceLink = comp;

            if (discipline != null)
                dc_link.DisciplineLink = discipline;

            _storage.Update(dc_link);

            return dc_link;
        }
    }
}
