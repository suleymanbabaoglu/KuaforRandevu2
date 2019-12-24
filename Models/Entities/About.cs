using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KuaforRandevu2.Models.Entities
{
    public partial class About
    {
        [Key]
        public int Id { get; set; }
        public string Mission { get; set; }
        public string Vision { get; set; }
        public string AboutText { get; set; }
    }
}
