using System;
using System.Collections.Generic;

namespace KuaforRandevu2.Models.Entities
{
    public partial class User
    {
        public User()
        {
            Appointment = new HashSet<Appointment>();
            Expert = new HashSet<Expert>();
            Gallery = new HashSet<Gallery>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<Expert> Expert { get; set; }
        public virtual ICollection<Gallery> Gallery { get; set; }
    }
}
