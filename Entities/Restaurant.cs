namespace RestaurantAPI.Entities
{
    public class Restaurant
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
    }
}
