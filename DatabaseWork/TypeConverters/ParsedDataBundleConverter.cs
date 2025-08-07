using DatabaseWork.DataClasses;
using DatabaseWork.Interfaces;
using DatabaseWork.TypeConverters.DataClasses;
using DocsParserLib.DataClasses;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatabaseWork.TypeConverters
{
    public class ParsedDataBundleConverter : ILibTypeConverterConnect<ParsedDataBundle, ConvertedDataBundle, Profile>
    {
        public ConvertedDataBundle Data { get; }

        private DatabaseContext? database;

        public ParsedDataBundleConverter()
        {
            Data = new ConvertedDataBundle();
        }

        public ParsedDataBundleConverter(DatabaseContext context) : this()
        {
            database = context;
        }

        public ConvertedDataBundle Convert(ParsedDataBundle type, Profile profile)
        {
            GetDisciplinesAndCompetences(type, profile);
            GetTasks(type);

            return Data;
        }

        private void GetTasks(ParsedDataBundle type)
        {
            QuestionConverter q_converter = new();
            PracticTaskConverter pt_converter = new();

            Data.TDCLinks.AddRange(q_converter.ConvertAll(type.Questions, Data.DCLink));
            Data.TDCLinks.AddRange(pt_converter.ConvertAll(type.PracticTasks, Data.DCLink));

            Data.Tasks.AddRange(q_converter.Tasks);
            Data.Tasks.AddRange(pt_converter.Tasks);

            Data.SelectedItems.AddRange(pt_converter.AnswerVariants);
        }

        private void GetDisciplinesAndCompetences(ParsedDataBundle type, Profile profile)
        {
            CompetentionConverter c_converter = new();
            DisciplineConverter d_converter = new();

            GetDiscipline(type, d_converter);
            GetCompetences(type, profile, c_converter);

            Data.DCLink.AddRange(ConnectDisciplineToCompetence(c_converter.Competentions, d_converter.Disciplines));
        }

        private void GetDiscipline(ParsedDataBundle type, DisciplineConverter d_converter)
        {
            if (type.Discipline != null)
            {
                var discipl = d_converter.Convert(type.Discipline);

                var item_ = IsHasItem(database.Disciplines, e => e.DisTitle.Equals(discipl.DisTitle, StringComparison.OrdinalIgnoreCase));

                if (item_ is not null)
                    Data.Discplines.Add(item_);

                else
                    Data.Discplines.Add(discipl);
            }
        }

        private void GetCompetences(ParsedDataBundle type, Profile profile, CompetentionConverter c_converter)
        {
            var competences = c_converter.ConvertAll(type.Competentions, profile);

            foreach (var comp in competences)
            {
                var check_item = IsHasItem(database.Competences, e => e.CompNumber.Equals(comp.CompNumber, StringComparison.OrdinalIgnoreCase);

                if (check_item is not null)
                    Data.Competences.Add(check_item);

                else
                    Data.Competences.Add(comp);
            }
        }

        private List<DisciplineCompetenceLink> ConnectDisciplineToCompetence(List<Competence> competences, List<DatabaseWork.DataClasses.Discipline> disciplines)
        {
            List<DisciplineCompetenceLink> dc = new List<DisciplineCompetenceLink>();

            foreach (var discipline in disciplines)
            {
                foreach (var competence in competences)
                {
                    DisciplineCompetenceLink link = new DisciplineCompetenceLink { DisciplineLink = discipline, CompetenceLink = competence };
                    dc.Add(link);
                }
            }

            return dc;
        }

        private T? IsHasItem<T>(DbSet<T> collection, Expression<Func<T, bool>> object_finder)
            where T : class
        {
            var finded_item = collection.FirstOrDefault(object_finder);

            return finded_item;
        }

        public List<ConvertedDataBundle> ConvertAll(List<ParsedDataBundle> list)
        {
            throw new NotImplementedException();
        }

        public List<ConvertedDataBundle> ConvertAll(List<ParsedDataBundle> list, Profile connect)
        {
            throw new NotImplementedException();
        }

        public ConvertedDataBundle Convert(ParsedDataBundle type)
        {
            throw new NotImplementedException();
        }
    }
}
