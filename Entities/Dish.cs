namespace RestaurantAPI
{
    public class Dish
    {
        public Dish(int _id, string _name, string _type, decimal _price)
        {
            id = _id;
            name = _name;
            type = _type;
            price = _price;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public decimal price { get; set; }
    }
}
