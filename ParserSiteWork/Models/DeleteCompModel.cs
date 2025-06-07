using DatabaseWork.DataClasses;
using Microsoft.AspNetCore.Mvc;

namespace ParserSiteWork.Models
{
    public class DeleteCompModel
    {
        public Competence? Competence { get; set; }
        public bool ToDelete { get; set; }
    }
}
