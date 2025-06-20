﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.Interfaces
{
    public interface ILibTypeConverter<T, K>
    {
        K Convert(T type);
        List<K> ConvertAll(List<T> list);
    }

    public interface ILibTypeConverterConnect<T, K, C> : ILibTypeConverter<T, K>
    {
        K Convert(T type, C connect);
        List<K> ConvertAll(List<T> list, C connect);
    }
}
