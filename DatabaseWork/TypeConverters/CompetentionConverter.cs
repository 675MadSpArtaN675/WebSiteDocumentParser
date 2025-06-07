using DatabaseWork.DataClasses;
using DatabaseWork.Interfaces;
using DocsParserLib.DataClasses;

namespace DatabaseWork.TypeConverters
{
    public class CompetentionConverter : ILibTypeConverter<Competention, Competence>
    {
        public List<Competence> Competentions { get; }

        public CompetentionConverter()
        {
            Competentions = new List<Competence>();
        }

        public Competence Convert(Competention type)
        {
            Competence competence = new Competence
            {
                CompNumber = type.Name,
                CompAnnotation = type.Description
            };

            Competentions.Add(competence);

            return competence;
        }

        public List<Competence> ConvertAll(List<Competention> list)
        {
            foreach (var item in list)
            {
                Convert(item);
            }

            return Competentions;
        }
    }
}
