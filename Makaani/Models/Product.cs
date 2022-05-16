using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class Product
    {
        public Product()
        {
            Ads = new HashSet<Ads>();
            Feature = new HashSet<Feature>();
            LovedProductList = new HashSet<LovedProductList>();
            Media = new HashSet<Media>();
            PayingOffer = new HashSet<PayingOffer>();
            Rate = new HashSet<Rate>();
        }

        public int ProductId { get; set; }
        public double? Area { get; set; }
        public int? BathRooms { get; set; }
        public int? BedRooms { get; set; }
        public int? LundrayRoom { get; set; }
        public int? LivingRoom { get; set; }
        public int? Grage { get; set; }
        public int? Kitchen { get; set; }
        public int? Store { get; set; }
        public bool? IshaveBalcon { get; set; }
        public bool? IshaveGarden { get; set; }
        public bool? IshaveSwimPool { get; set; }
        public bool? IshaveElvator { get; set; }
        public int? FloorNumber { get; set; }
        public int? FinishesId { get; set; }
        public bool? IsHasManyFloor { get; set; }
        public int? FloorSum { get; set; }
        public bool? IsHasFurniture { get; set; }
        public int? OwnerId { get; set; }

        public virtual Finishes Finishes { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Ads> Ads { get; set; }
        public virtual ICollection<Feature> Feature { get; set; }
        public virtual ICollection<LovedProductList> LovedProductList { get; set; }
        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<PayingOffer> PayingOffer { get; set; }
        public virtual ICollection<Rate> Rate { get; set; }
    }
}
