namespace RestaurantAPI.Models
{
    public class RestaurantDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string postal_code { get; set; }
    }
}
