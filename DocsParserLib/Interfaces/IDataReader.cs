using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsParserLib.Interfaces
{
    /// <summary>
    /// Метод, который позволяет получить входные данные для парсера
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataReader<T>
    {
        T? GetData();
    }
}
