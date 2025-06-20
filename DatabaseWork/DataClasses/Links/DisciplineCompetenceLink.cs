﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses
{
    public class DisciplineCompetenceLink
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDdc { get; set; }

        [Key]
        public Competence CompetenceLink { get; set; }
        [Key]
        public Discipline DisciplineLink { get; set; }

        public List<TaskDesciplineCompetenceLink> TDCLink { get; set; }
    }
}
