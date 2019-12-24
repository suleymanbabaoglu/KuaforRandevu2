using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KuaforRandevu2.Models.Entities
{
    public partial class Gallery
    {
        [Key]
        public int Id { get; set; }
        public string ImgText { get; set; }
        [Required]
        public string ImgPath { get; set; }
        public DateTime ImgDate { get; set; }
        public int ImgUserId { get; set; }

        public virtual User ImgUser { get; set; }
    }
}
