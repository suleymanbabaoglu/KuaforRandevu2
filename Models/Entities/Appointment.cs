using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuaforRandevu2.Models.Entities
{
    public partial class Appointment
    {
        [Key]        
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ExpertId { get; set; }
        public DateTime AppointmentDate { get; set; }

        [ForeignKey("ExpertId")]
        public virtual Expert Expert { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
