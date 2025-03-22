using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.Interfaces
{
    public interface IDataWriter<T>
    {
        void Write(T object_to_write);
    }
}
