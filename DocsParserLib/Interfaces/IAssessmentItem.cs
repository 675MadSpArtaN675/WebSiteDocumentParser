namespace DocsParserLib.Interfaces
{
    public interface IAssessmentItem
    {
        /// <summary>
        /// Номер элемента (от 0)
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Описание элемента
        /// </summary>
        public string Description { get; set; }
    }
}
