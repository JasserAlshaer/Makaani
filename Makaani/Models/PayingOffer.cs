using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class PayingOffer
    {
        public int PayingOfferId { get; set; }
        public string Title { get; set; }
        public double? ProvidedPrice { get; set; }
        public string Note { get; set; }
        public DateTime? OfferDate { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
