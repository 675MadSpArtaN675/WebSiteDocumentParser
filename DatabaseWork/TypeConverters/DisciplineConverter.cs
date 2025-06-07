using DatabaseWork.DataClasses;
using DatabaseWork.Interfaces;
using DocsParserLib.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.TypeConverters
{
    using disc = DocsParserLib.DataClasses.Discipline;
    using disc_table = DatabaseWork.DataClasses.Discipline;

    public class DisciplineConverter : ILibTypeConverter<disc, disc_table>
    {
        public List<disc_table> Disciplines { get; }

        public DisciplineConverter()
        {
            Disciplines = new List<disc_table>();
        }

        public disc_table Convert(disc type)
        {
            disc_table result = new disc_table
            {
                DisTitle = type.Name
            };

            Disciplines.Add(result);

            return result;
        }

        public List<disc_table> ConvertAll(List<disc> list)
        {
            foreach (disc disc in list)
            {
                Convert(disc);
            }

            return Disciplines;
        }
    }
}
