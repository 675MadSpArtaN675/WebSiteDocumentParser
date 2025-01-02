using DocsParserLib.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsParserLib.Interfaces
{
    public interface ICompetencinable
    {
        /// <summary>
        /// Компетенция, которой привязан экземпляр
        /// </summary>
        public Competention Competention { get; set; }
    }
}
