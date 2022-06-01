namespace Makaani.Models
{
    public class EstateMain
    {
        public Product Product { get; set; }    
        public Media Media { get; set; }
        public Finishes Finishes { get; set; }  

        public User User { get; set; }  
        public Ads Ads { get; set; }
        public Department Department { get; set; }  

        public Location Location { get; set; }

        public Category Category { get; set; }

        public  Provinces Provinces { get; set; }
    }
}
