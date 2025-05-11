using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses
{
    public class SelectedItems
    {
        public int IDSelect { get; set; }
        public string? SelectValue { get; set; }
        public bool SelectTrue { get; set; }
    }

    public class ItemsAccordance
    {
        public int IDia { get; set; }
        public string? IAText { get; set; }
    }

    public class FirstPartAccordance
    {
        public int IDfpa { get; set; }
        public string? FPANumber { get; set; }
        public string? FPAValue { get; set; }
    }

    public class SecondPartAccordance
    {
        public int IDspa { get; set; }
        public string? SPANumber { get; set; }
        public string? SPAValue { get; set; }


    }
}
