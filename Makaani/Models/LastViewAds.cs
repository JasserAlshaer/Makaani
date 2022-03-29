using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class LastViewAds
    {
        public int LastViewAdsId { get; set; }
        public int? UserId { get; set; }
        public int? AdsId { get; set; }
        public DateTime? LastViewAt { get; set; }
        public bool? IsActive { get; set; }

        public virtual Ads Ads { get; set; }
        public virtual User User { get; set; }
    }
}
