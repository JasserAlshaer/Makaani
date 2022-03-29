using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class UserSearch
    {
        public int UserSearchId { get; set; }
        public string SearchTitle { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsFaviourate { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
