using DocsParserLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsParserLib.DataClasses
{
    /// <summary>
    /// Структура, представляющая вопрос.
    /// </summary>
    public class Question : IAssessmentItem, ICompetencinable
    {
        public int Number { get; set; }
        public string Description { get; set; }
        public Competention Competention { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Rectangle"/>.
        /// </summary>
        /// 
        /// <param name="_num">Номер вопроса (от 0)</param>
        /// <param name="_comp_name">Компетенция, к которой вопрос относиться</param>
        /// <param name="_descr">Описание вопроса</param>
        public Question(int _num, Competention _comp_name, string _descr)
        {
            Number = _num;
            Competention = _comp_name;
            Description = _descr;
        }

        public Question() : this(1, new Competention(), "")
        { }

        public override string ToString()
        {
            return $"Номер: {Number}\nНазвание компетенции: {Competention.Name}\nОписание вопроса: {Description}\n";
        }
    }
}
