using System;
using System.Collections.Generic;

namespace KuaforRandevu2.Models.Entities
{
    public partial class Gallery
    {
        public int Id { get; set; }
        public string ImgText { get; set; }
        public string ImgPath { get; set; }
        public DateTime ImgDate { get; set; }
        public int ImgUserId { get; set; }

        public virtual User ImgUser { get; set; }
    }
}
