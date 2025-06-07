using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.Interfaces;
using DocsParserLib.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.TypeConverters.UtilityTypes
{
    public class AnswerVariantConverter : ILibTypeConverter<AnswerVariant, SelectedItems>
    {
        public List<SelectedItems> SelectedItemsConverted { get; }
        public AnswerVariantConverter()
        {
            SelectedItemsConverted = new List<SelectedItems>();
        }

        public SelectedItems Convert(AnswerVariant answerVariant)
        {
            SelectedItems si = new SelectedItems()
            {
                SelectValue = answerVariant.Description,
                SelectTrue = answerVariant.ValidAnswer
            };

            SelectedItemsConverted.Add(si);

            return si;
        }

        public List<SelectedItems> ConvertAll(List<AnswerVariant> answerVariant)
        {
            foreach (var element in answerVariant)
            {
                Convert(element);
            }

            return SelectedItemsConverted;
        }
    }
}
