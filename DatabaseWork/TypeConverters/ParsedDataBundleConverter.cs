using DatabaseWork.Interfaces;
using DatabaseWork.TypeConverters.DataClasses;
using DocsParserLib.DataClasses;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.TypeConverters
{
    public class ParsedDataBundleConverter : ILibTypeConverter<ParsedDataBundle, ConvertedDataBundle>
    {
        public ConvertedDataBundle Data { get; }

        public ParsedDataBundleConverter()
        {
            Data = new ConvertedDataBundle();
        }

        public ConvertedDataBundle Convert(ParsedDataBundle type)
        {
            CompetentionConverter c_converter = new();
            QuestionConverter q_converter = new();
            PracticTaskConverter pt_converter = new();
            DisciplineConverter d_converter = new();

            Data.Discplines.Add(d_converter.Convert(type.Discipline));

            Data.Competences.AddRange(c_converter.ConvertAll(type.Competentions));

            Data.Tasks.AddRange(q_converter.ConvertAll(type.Questions));
            Data.Tasks.AddRange(pt_converter.ConvertAll(type.PracticTasks));

            Data.SelectedItems.AddRange(pt_converter.AnswerVariants);

            return Data;
        }

        public List<ConvertedDataBundle> ConvertAll(List<ParsedDataBundle> list)
        {
            throw new NotImplementedException();
        }
    }
}
