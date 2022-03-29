using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class User
    {
        public User()
        {
            Ads = new HashSet<Ads>();
            FollowerFirstUser = new HashSet<Follower>();
            FollowerSecondUser = new HashSet<Follower>();
            LastViewAds = new HashSet<LastViewAds>();
            Login = new HashSet<Login>();
            LoveList = new HashSet<LoveList>();
            PayingOffer = new HashSet<PayingOffer>();
            Rate = new HashSet<Rate>();
            Testimonails = new HashSet<Testimonails>();
            UserSearch = new HashSet<UserSearch>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string WhatsupLink { get; set; }
        public string ProfileImage { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual ICollection<Ads> Ads { get; set; }
        public virtual ICollection<Follower> FollowerFirstUser { get; set; }
        public virtual ICollection<Follower> FollowerSecondUser { get; set; }
        public virtual ICollection<LastViewAds> LastViewAds { get; set; }
        public virtual ICollection<Login> Login { get; set; }
        public virtual ICollection<LoveList> LoveList { get; set; }
        public virtual ICollection<PayingOffer> PayingOffer { get; set; }
        public virtual ICollection<Rate> Rate { get; set; }
        public virtual ICollection<Testimonails> Testimonails { get; set; }
        public virtual ICollection<UserSearch> UserSearch { get; set; }
    }
}
