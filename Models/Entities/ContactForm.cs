using System;
using System.Collections.Generic;

namespace KuaforRandevu2.Models.Entities
{
    public partial class ContactForm
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
