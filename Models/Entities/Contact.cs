using System;
using System.Collections.Generic;

namespace KuaforRandevu2.Models.Entities
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string GooglePlus { get; set; }
    }
}
