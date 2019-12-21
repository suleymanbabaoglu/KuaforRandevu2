using System;
using System.Collections.Generic;

namespace KuaforRandevu2.Models.Entities
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExpertId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public virtual Expert Expert { get; set; }
        public virtual User User { get; set; }
    }
}
