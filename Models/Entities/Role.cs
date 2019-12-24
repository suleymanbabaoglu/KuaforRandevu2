using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KuaforRandevu2.Models.Entities
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
