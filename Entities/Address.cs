namespace RestaurantAPI
{
    public class Address
    {
        public Address(string _city, string _street, string _postal_code, int _id = 0)
        {
            id = _id;
            city = _city;
            street = _street;
            postal_code = _postal_code;
        }

        public int id { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string postal_code { get; set; }
    }
}
