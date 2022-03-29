using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class Offer
    {
        public Offer()
        {
            Ads = new HashSet<Ads>();
        }

        public int OfferId { get; set; }
        public string Name { get; set; }
        public double? SalePercent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Ads> Ads { get; set; }
    }
}
