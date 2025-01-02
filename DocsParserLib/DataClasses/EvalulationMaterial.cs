namespace DocsParserLib.DataClasses
{
    /// <summary>
    /// Структура, представляющая показатель оценивания
    /// </summary>
    public class EvalulationMaterial
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

        public EvalulationMaterial() : this("", "", "", "")
        { }

        public override string ToString()
        {
            return $"Имя: {Name}\nОписание: {Description}\nТип оценочного материала: {EM_Type}\nПоказатели оценивания: {EvalulationIndicator}\n";
        }
    }
}
