using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseWork.DataClasses;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;

namespace DatabaseWork.DataProcessors.StandartProcessors.CompetenceProcessors
{
    public class TypeCompetenceService : AbstractService<TypeCompetence>
    {
        public TypeCompetenceService(DatabaseContext context) : base(context, context.TypesOfCompetences)
        { }

        public override TypeCompetence Add(TypeCompetence entity)
        {
            int free_id = FindFreeNumber(tc => tc.IDtc);
            entity.IDtc = free_id;

            _storage.Add(entity);

            return entity;
        }
    }
}
