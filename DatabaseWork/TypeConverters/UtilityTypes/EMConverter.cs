using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DatabaseWork.Interfaces;
using DatabaseWork.DataClasses;
using DocsParserLib.DataClasses;

namespace DatabaseWork.TypeConverters.UtilityTypes
{
    public class EMConverter : ILibTypeConverter<EvalulationMaterial, TypeCompetence>
    {
        public TypeCompetence Convert(EvalulationMaterial type)
        {
            throw new NotImplementedException();
        }

        public List<TypeCompetence> ConvertAll(List<EvalulationMaterial> list)
        {
            throw new NotImplementedException();
        }
    }
}
