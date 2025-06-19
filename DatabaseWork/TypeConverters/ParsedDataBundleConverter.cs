using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.DataProcessors.StandartProcessors;
using DatabaseWork.Interfaces;
using DatabaseWork.TypeConverters.DataClasses;
using DocsParserLib.DataClasses;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.TypeConverters
{
    public class ParsedDataBundleConverter : ILibTypeConverterConnect<ParsedDataBundle, ConvertedDataBundle, Profile>
    {
        public ConvertedDataBundle Data { get; }

        public ParsedDataBundleConverter()
        {
            Data = new ConvertedDataBundle();
        }

        public ConvertedDataBundle Convert(ParsedDataBundle type, Profile profile)
        {
            CompetentionConverter c_converter = new();
            QuestionConverter q_converter = new();
            PracticTaskConverter pt_converter = new();
            DisciplineConverter d_converter = new();

            if (type.Discipline != null)
                Data.Discplines.Add(d_converter.Convert(type.Discipline));

            foreach (var item in Data.Discplines)
                System.Console.WriteLine(item.DisTitle);

            Data.Competences.AddRange(c_converter.ConvertAll(type.Competentions, profile));
            Data.DCLink.AddRange(ConnectDisciplineToCompetence(c_converter.Competentions, d_converter.Disciplines));

            Data.TDCLinks.AddRange(q_converter.ConvertAll(type.Questions, Data.DCLink));
            Data.TDCLinks.AddRange(pt_converter.ConvertAll(type.PracticTasks, Data.DCLink));

            Data.Tasks.AddRange(q_converter.Tasks);
            Data.Tasks.AddRange(pt_converter.Tasks);

            Data.SelectedItems.AddRange(pt_converter.AnswerVariants);

            return Data;
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
