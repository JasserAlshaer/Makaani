using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class LoveList
    {
        public LoveList()
        {
            LovedProductList = new HashSet<LovedProductList>();
        }

        public int LoveListId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<LovedProductList> LovedProductList { get; set; }
    }
}
