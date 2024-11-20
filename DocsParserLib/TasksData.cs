namespace DocsParserLib
{
    /// <summary>
    /// Структура, представляющая показатель оценивания
    /// </summary>
    public struct EvalulationMaterial
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string EM_Type { get; set; }
        public string EvalulationIndicator { get; set; }

        /// <summary>
        /// Инициализация экземпляра структуры <see cref="EvalulationMaterial"/>
        /// </summary>
        /// 
        /// <param name="name">Название показателя (Например, знать, уметь, владеть, и т.д.</param>
        /// <param name="description">Описание показателя</param>
        /// <param name="em_type">Тип показателя оценивания</param>
        /// <param name="evalulation_indicator">Показатель оценивания (например, Наличие умений, полнота знаний, и т.д.)</param>
        public EvalulationMaterial(string name, string description, string em_type, string evalulation_indicator)
        {
            Name = name;
            Description = description;
            EM_Type = em_type;
            EvalulationIndicator = evalulation_indicator;
        }

        public override string ToString()
        {
            return $"Имя: {Name}\nОписание: {Description}\nТип оценочного материала: {EM_Type}\nПоказатели оценивания: {EvalulationIndicator}\n";
        }
    }

    /// <summary>
    /// Структура, представляющая компетенцию
    /// </summary>
    public struct Competention
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public List<EvalulationMaterial> EvalulationMaterial { get; private set; }

        /// <summary>
        /// Инициализация экземпляра структуры <see cref="Competention"/>
        /// </summary>
        /// <param name="_number">Порядковый номер компетенции</param>
        /// <param name="_name">Название компетенции (Например, ПК-2)</param>
        /// <param name="e_mat">Список оценочных критериев. Представляет List экземпляров структуры <see cref="EvalulationMaterial"/></param>
        public Competention(int _number, string _name, List<EvalulationMaterial> e_mat)
        {
            Number = _number;
            Name = _name;
            EvalulationMaterial = e_mat;
        }

        /// <summary>
        /// Инициализация экземпляра структуры <see cref="Competention"/>
        /// </summary>
        /// <param name="_number">Порядковый номер компетенции</param>
        /// <param name="_name">Название компетенции (Например, ПК-2)</param>
        public Competention(int _number, string _name) : this(_number, _name, new List<EvalulationMaterial>()) { }

        /// <summary>
        /// Инициализация экземпляра структуры <see cref="Competention"/>
        /// </summary>
        public Competention() : this(1, "", new List<EvalulationMaterial>()) { }

        public override string ToString()
        {
            string list_em = String.Empty;

            foreach (EvalulationMaterial item in EvalulationMaterial)
                list_em += $"{item.ToString()}\n";

            return $"Номер: {Number}\nКомпетенция: {Name}\nСписок оценочных критериев:\n\n{list_em}\n";
        }
    }

    /// <summary>
    /// Структура, представляющая вопрос.
    /// </summary>
    public struct Question : IAssessmentItem, ICompetencinable
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

        public override string ToString()
        {
            return $"Номер: {Number}\nНазвание компетенции: {Competention.Name}\nОписание вопроса: {Description}\n";
        }
    }

    /// <summary>
    /// Структура, представляющий вариант ответа в практическом задании
    /// </summary>
    public struct AnswerVariant : IAssessmentItem
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
        public char AnswerLetter { 
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
        }

        private string Letters = "АБВГДЕЁЖЗИЙКЛМНОПРСТ";

        /// <summary>
        /// Инициализирует экземпляр структуры <see cref="AnswerVariant">
        /// </summary>
        /// <param name="_num">Номер варианта ответа (от 0)</param>
        /// <param name="_desc">Описание задания</param>
        /// <param name="valid">Флаг, обозначающий является ли данный вариант ответа правильным</param>
        public AnswerVariant(int _num, string _desc, bool valid)
        {
            Number = _num;
            Description = _desc;
            ValidAnswer = valid;
        }

        public override string ToString()
        {
            return $"[{Number}/{AnswerNormalNumber}/{AnswerLetter}]:\nDescription: {Description}\nValid: {ValidAnswer}";
        }
    }

    /// <summary>
    /// Структура, обозначающая практическое задание
    /// </summary>
    public struct PracticTask : IAssessmentItem, ICompetencinable
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
