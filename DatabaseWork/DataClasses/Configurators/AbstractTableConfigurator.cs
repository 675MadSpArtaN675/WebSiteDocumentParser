using DatabaseWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses.Configurators
{
    public class AbstractTableConfigurator : ITableGroupConfigurator
    {
        protected ModelBuilder _builder;

        public AbstractTableConfigurator(ModelBuilder builder)
        {
            _builder = builder;
        }

        public virtual void Configure()
        {
            throw new NotImplementedException();
        }
    }
}
