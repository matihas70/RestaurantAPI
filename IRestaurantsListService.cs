namespace RestaurantAPI
{
    public interface IRestaurantsListService
    {
        public string Get();
        public IEnumerable<Restaurant> GetRestaurants();
        public IEnumerable<Dish> GetDishes(int id);
        public string PostRestaurant(Address address, Restaurant restaurant);
        public string PostDish(Dish dish, int id);
        public string DeleteRestaurant(int id);
        public string DeleteDish(int id);
    }
}
