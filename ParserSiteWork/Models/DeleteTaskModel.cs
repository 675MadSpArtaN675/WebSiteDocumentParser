using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ParserSiteWork.Models
{
    public class DeleteTaskModel
    {
        public Task_d? Task { get; set; }
        public bool ToDelete { get; set; }
    }
}
