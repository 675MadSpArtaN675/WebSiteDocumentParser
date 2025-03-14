using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsParserLib.Exceptions
{
    public class EmptyPageException : Exception
    {
        public EmptyPageException(string message) : base(message) { }
        public EmptyPageException() : base("Обнаружена пустая страница!") { }
    }
}
