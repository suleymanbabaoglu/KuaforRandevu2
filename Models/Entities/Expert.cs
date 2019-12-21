using System;
using System.Collections.Generic;

namespace KuaforRandevu2.Models.Entities
{
    public partial class Expert
    {
        public Expert()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string ExpertJob { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
