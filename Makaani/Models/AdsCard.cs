namespace Makaani.Models
{
    public class AdsCard
    {
        public EstateMain EstateMain {get;set;}
        public User User {get;set;}
        public Department Department {get;set;}
        public Category Category {get;set;} 
        public Location Location {get;set;} 

        public Ads Ads {get;set;}
    }
}
