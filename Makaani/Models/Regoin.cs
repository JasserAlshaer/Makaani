using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class Regoin
    {
        public int RegoinId { get; set; }
        public string Name { get; set; }
        public int? ProvincesId { get; set; }

        public virtual Provinces Provinces { get; set; }
    }
}
