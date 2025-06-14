using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;

namespace ParserSiteWork.Models
{
    public class DisplayModel
    {
        public List<SelectedItems> SelectedItems;
        public List<TaskDesciplineCompetenceLink> TaskCompetenceDisciplineData;
    }
}