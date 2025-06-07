using DatabaseWork.DataClasses;
using Microsoft.AspNetCore.Mvc;

namespace ParserSiteWork.Models
{
    public class DeleteProfileModel
    {
        public Profile? Profile { get; set; }
        public bool ToDelete { get; set; }
    }
}
