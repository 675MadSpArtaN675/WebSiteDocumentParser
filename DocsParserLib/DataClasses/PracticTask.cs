using DocsParserLib.Interfaces;

namespace DocsParserLib.DataClasses
{
    /// <summary>
    /// Структура, обозначающая практическое задание
    /// </summary>
    public class PracticTask : IAssessmentItem, ICompetencinable
    {
        /// <inheritdoc/>
        public int Number { get; set; }
        /// <inheritdoc/>
        public string Description { get; set; }

        /// <summary>
        /// Указывает на компетенцию, к которой привязано практическое задание
        /// </summary>
        public Competention Competention { get; set; }

        /// <summary>
        /// Указывает на список вариантов ответа
        /// </summary>
        public List<AnswerVariant> answerVariants { get; set; }

        /// <summary>
        /// Инициализирует экземпляр структуры <see cref="PracticTask"/>. 
        /// </summary>
        /// <param name="_numb">Номер задания (от 0)</param>
        /// <param name="_competetion">Компетенция, к которой относится задание. <seealso cref="Competention"/></param>
        /// <param name="_descr">Описание задания</param>
        /// <param name="answers">Список вариантов ответа. <seealso cref="AnswerVariant"></seealso></param>
        public PracticTask(int _numb, Competention _competetion, string _descr, List<AnswerVariant> answers)
        {
            Number = _numb;
            Competention = _competetion;
            Description = _descr;
            answerVariants = answers;
        }

        /// <summary>
        /// Инициализирует экземпляр структуры <see cref="PracticTask"/>. 
        /// </summary>
        /// <param name="_numb">Номер задания (от 0)</param>
        /// <param name="_competetion">Компетенция, к которой относится задание. <seealso cref="Competention"/></param>
        /// <param name="_descr">Описание задания</param>
        /// <param name="answers">Список вариантов ответа. <seealso cref="AnswerVariant"></seealso></param>
        public PracticTask(int _numb, Competention _competetion, string _descr) : this(_numb, _competetion, _descr, new List<AnswerVariant>()) { }

        public PracticTask() : this(1, new Competention(), "")
        { }

        /// <summary>
        /// Получение правильного ответа на задание
        /// </summary>
        /// <returns>
        /// Экземпляр класса <see cref="AnswerVariant"/>, который является правильным ответом на задание (В котором флаг ValidAnswer равен true)
        /// </returns>
        public AnswerVariant GetValidVariant()
        {
            return answerVariants.First(n => n.ValidAnswer);
        }

        public override string ToString()
        {
            string answers = "";
            foreach (var variant in answerVariants)
                answers += variant.ToString() + "\n\n";

            return $"{Number} {Competention.Name} {Description}\n{answers}";
        }
    }
}
