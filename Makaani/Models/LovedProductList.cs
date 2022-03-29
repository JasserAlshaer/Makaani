using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class LovedProductList
    {
        public int LovedProductListId { get; set; }
        public int? LoveListId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? AddedAt { get; set; }

        public virtual LoveList LoveList { get; set; }
        public virtual Product Product { get; set; }
    }
}
