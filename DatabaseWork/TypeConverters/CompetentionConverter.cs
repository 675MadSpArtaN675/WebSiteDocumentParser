using DatabaseWork.DataClasses;
using DatabaseWork.Interfaces;
using DocsParserLib.DataClasses;

namespace DatabaseWork.TypeConverters
{
    public class CompetentionConverter : ILibTypeConverterConnect<Competention, Competence, Profile>
    {
        public List<Competence> Competentions { get; }

        public CompetentionConverter()
        {
            Competentions = new List<Competence>();
        }

        public Competence Convert(Competention type, Profile? profile)
        {
            Competence competence = new Competence
            {
                CompNumber = type.Name,
                CompAnnotation = type.Description,
            };

            if (profile != null)
                competence.ProfileLink = profile;

            Competentions.Add(competence);

            return competence;
        }

        public List<Competence> ConvertAll(List<Competention> list, Profile? profile)
        {
            foreach (var item in list)
            {
                Convert(item, profile);
            }

            return Competentions;
        }

        public Competence Convert(Competention type)
        {
            throw new NotImplementedException();
        }

        public List<Competence> ConvertAll(List<Competention> list)
        {
            throw new NotImplementedException();
        }
    }
}
