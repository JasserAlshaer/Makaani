using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class Location
    {
        public Location()
        {
            Ads = new HashSet<Ads>();
        }

        public int LoactionId { get; set; }
        public string LoactionLabel { get; set; }
        public string MapsLink { get; set; }
        public int? RegoinId { get; set; }
        public int? ProvincesId { get; set; }
        public string Notes { get; set; }

        public virtual Provinces Provinces { get; set; }
        public virtual ICollection<Ads> Ads { get; set; }
    }
}
