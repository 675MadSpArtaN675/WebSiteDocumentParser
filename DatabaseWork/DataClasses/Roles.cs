using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses
{
    public class Role
    {
        public int IDrol { get; set; }
        public string Name { get; set; }

        public List<User>? UserLink { get; set; }
    }
}
