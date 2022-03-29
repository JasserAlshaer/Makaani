using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            Ads = new HashSet<Ads>();
        }

        public int PromotionId { get; set; }
        public string Type { get; set; }
        public int? Duration { get; set; }
        public DateTime? StartFrom { get; set; }
        public DateTime? EndAt { get; set; }
        public double? Cost { get; set; }

        public virtual ICollection<Ads> Ads { get; set; }
    }
}
