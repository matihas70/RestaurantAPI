namespace RestaurantAPI.Entities
{
    public class Restaurant
    {
        public Restaurant(string _name, string _category, string _email, Address _address, int _id = 0)
        {
            id = _id;
            name = _name;
            category = _category;
            email = _email;
            address = _address;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
    }
}
