using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        

        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<Expert> Expert { get; set; }
        public virtual ICollection<Gallery> Gallery { get; set; }
    }
}
