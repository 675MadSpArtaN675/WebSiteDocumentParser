using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses
{
    public class User
    {
        public int IDuser { get; set; }
        public string UserName{ get; set; }
        public string Password{ get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public Role? RoleLink { get; set; }
    }
}
