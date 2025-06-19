namespace DocsParserLib.DataClasses
{
    /// <summary>
    /// Структура, представляющая компетенцию
    /// </summary>
    public class Competention
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
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
        public Competention(string _name, int _number = 1) : this(_number, _name, new List<EvalulationMaterial>()) { }

        /// <summary>
        /// Инициализация экземпляра структуры <see cref="Competention"/>
        /// </summary>
        public Competention() : this(1, "", new List<EvalulationMaterial>()) { }

        public override string ToString()
        {
            string list_em = string.Empty;

            foreach (EvalulationMaterial item in EvalulationMaterial)
                list_em += $"{item.ToString()}\n";

            return $"Номер: {Number}\nКомпетенция: {Name}\nСписок оценочных критериев:\n\n{list_em}\n";
        }
    }
}
