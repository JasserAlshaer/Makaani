namespace Makaani.Models
{
    public class UsersPayingOffers
    {
        public User User { get; set; }
        public Ads Ads { get; set; }
        public PayingOffer PayingOffer { get; set; }
        public Product Product { get; set; }
    }
}
