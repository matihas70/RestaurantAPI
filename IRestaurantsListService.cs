namespace RestaurantAPI
{
    public interface IRestaurantsListService
    {
        public string Get();
        public IEnumerable<Restaurant> Get2();
        public string Post(Address address, Restaurant restaurant);
        public string Delete(int id);
    }
}
