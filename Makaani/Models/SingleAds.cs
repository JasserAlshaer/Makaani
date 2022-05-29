namespace Makaani.Models
{
    public class SingleAds
    {

        public Ads Ads { get; set; }
        public Location Location { get; set; }  
        public Category Category { get; set; }  
        public Department Department { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public Feature Feature { get; set; }
        public Finishes Finishes { get; set; }

        public Media Media { get; set; }    
    }
}
