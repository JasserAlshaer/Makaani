using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class Media
    {
        public int MediaId { get; set; }
        public string Path { get; set; }
        public bool? IsMainImage { get; set; }
        public int? ProductId { get; set; }
        public int? MediaTypeId { get; set; }

        public virtual MediaType MediaType { get; set; }
        public virtual Product Product { get; set; }
    }
}
