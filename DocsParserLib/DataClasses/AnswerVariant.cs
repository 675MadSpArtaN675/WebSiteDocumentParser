using DocsParserLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsParserLib.DataClasses
{
    /// <summary>
    /// Структура, представляющий вариант ответа в практическом задании
    /// </summary>
    public class AnswerVariant : IAssessmentItem
    {
        /// <inheritdoc/>
        public int Number { get; set; }
        /// <inheritdoc/>
        public string Description { get; set; }

        /// <summary>
        /// Указывает, является ли ответ правильным.
        /// </summary>
        public bool ValidAnswer { get; set; }

        /// <summary>
        /// Буква ответа. (Например, если ответ под номером 0, то его буква - А
        /// </summary>
        public char AnswerLetter
        {
            get
            {
                return Letters[Number];
            }
        }

        /// <summary>
        /// Удобочитаемый номер задания для человека. (Например, если ответ под номером 0, то его свойство вернёт 1)
        /// </summary>
        public int AnswerNormalNumber
        {
            get
            {
                return Number + 1;
            }

            set
            {
                Number = value - 1;
            }
        }

        private string Letters = "АБВГДЕЁЖЗИЙКЛМНОПРСТ";

        /// <summary>
        /// Инициализирует экземпляр структуры <see cref="AnswerVariant">
        /// </summary>
        /// <param name="_num">Номер варианта ответа (от 0)</param>
        /// <param name="_desc">Описание задания</param>
        /// <param name="valid">Флаг, обозначающий является ли данный вариант ответа правильным</param>
        public AnswerVariant(int _num, string _desc, bool valid = false)
        {
            Number = _num;
            Description = _desc;
            ValidAnswer = valid;
        }

        public AnswerVariant() : this(0, "", false) { }

        public override string ToString()
        {
            return $"[{Number}/{AnswerNormalNumber}/{AnswerLetter}]:\nDescription: {Description}\nValid: {ValidAnswer}";
        }
    }
}
