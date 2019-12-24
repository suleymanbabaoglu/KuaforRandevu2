using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuaforRandevu2.Models.Entities
{
    public partial class Expert
    {
        public Expert()
        {
            Appointment = new HashSet<Appointment>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public string ExpertJob { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
