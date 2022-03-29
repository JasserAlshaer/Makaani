using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class Provinces
    {
        public Provinces()
        {
            Location = new HashSet<Location>();
            Regoin = new HashSet<Regoin>();
        }

        public int ProvincesId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Location> Location { get; set; }
        public virtual ICollection<Regoin> Regoin { get; set; }
    }
}
