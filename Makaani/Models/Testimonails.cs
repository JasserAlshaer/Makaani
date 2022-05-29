using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class Testimonails
    {
        public int TestimonailsId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? RateCount { get; set; }
        public int? UserId { get; set; }
        public bool? IsAccepted { get; set; }

        public virtual User User { get; set; }
    }
}
