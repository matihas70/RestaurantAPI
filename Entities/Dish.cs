namespace RestaurantAPI.Entities
{
    public class Dish
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public decimal price { get; set; }
        public Restaurant restaurant { get; set; }
    }
}
