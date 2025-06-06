using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses.Tasks
{
    public class SelectedItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDSelect { get; set; }
        public string? SelectValue { get; set; }
        public bool SelectTrue { get; set; }

        [Required]
        public Task_d TaskLink { get; set; }
    }

    public class ItemsAccordance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDia { get; set; }
        public string? IAText { get; set; }

        [Required]
        public Task_d TaskLink { get; set; }
    }

    public class FirstPartAccordance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDfpa { get; set; }
        public string? FPANumber { get; set; }
        public string? FPAValue { get; set; }

        [Required]
        public Task_d TaskLink { get; set; }
    }

    public class SecondPartAccordance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDspa { get; set; }
        public string? SPANumber { get; set; }
        public string? SPAValue { get; set; }

        [Required]
        public Task_d TaskLink { get; set; }
    }
}
