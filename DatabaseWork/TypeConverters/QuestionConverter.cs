using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.Interfaces;
using DocsParserLib.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.TypeConverters
{
    public class QuestionConverter : ILibTypeConverter<Question, Task_d>
    {
        public List<Task_d> Tasks { get; }
        public QuestionConverter()
        {
            Tasks = new List<Task_d>();
        }

        public Task_d Convert(Question type)
        {
            Task_d task = new Task_d {
                TaskAnnotation = type.Description,
                TaskCorrectAnswer = "",
            };

            Tasks.Add(task);

            return task;
        }

        public List<Task_d> ConvertAll(List<Question> list)
        {
            foreach (var element in list)
            {
                Convert(element);
            }

            return Tasks;
        }
    }
}
