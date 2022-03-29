using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class Ads
    {
        public Ads()
        {
            LastViewAds = new HashSet<LastViewAds>();
        }

        public int AdsId { get; set; }
        public int? LocationId { get; set; }
        public int? CategoryId { get; set; }
        public int? DepartmentId { get; set; }
        public int? UserId { get; set; }
        public string Descrption { get; set; }
        public string Title { get; set; }
        public double? Price { get; set; }
        public DateTime? Date { get; set; }
        public int? OfferId { get; set; }
        public int? PromotionId { get; set; }
        public int? ProductId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Department Department { get; set; }
        public virtual Location Location { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual Product Product { get; set; }
        public virtual Promotion Promotion { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<LastViewAds> LastViewAds { get; set; }
    }
}
