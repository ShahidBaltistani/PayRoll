using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.Entities
{
   public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}
