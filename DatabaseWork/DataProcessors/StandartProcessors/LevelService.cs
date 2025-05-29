using DatabaseWork.DataClasses;
using DatabaseWork.DataProcessors.StandartProcessors.AbstractProcessors;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatabaseWork.DataProcessors.StandartProcessors
{
    public class LevelService : AbstractService<Level>
    {
        public LevelService(DatabaseContext context) : base(context, context.Levels)
        { }

        public override Level Add(Level level)
        {
            int free_id = FindFreeNumber(level => level.IDlv);

            level.IDlv = free_id;
            _storage.Add(level);

            return level;
        }
        public Level? UpdateLevelByTitle(Level level, string lv_title)
        {
            level.LvTitle = lv_title;
            
            return level;
        }

        public Level? UpdateLevelById(Level level, string lv_title)
        {
            level.LvTitle = lv_title;

            return level;
        }
    }
}
