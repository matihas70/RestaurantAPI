namespace RestaurantAPI
{
    public interface IRestaurantsListService
    {
        public string Get();
        public IEnumerable<Restaurant> Get2();
        public string PostRestaurant(Address address, Restaurant restaurant);
        public string PostDish(Dish dish, int id);
        public string Delete(int id);
    }
}
