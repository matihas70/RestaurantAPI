namespace RestaurantAPI
{
    public interface IRestaurantsListService
    {
        public string Get();
        public IEnumerable<Restaurant> GetRestaurants();
        public IEnumerable<Dish> GetDishes(int id);
        public bool PostRestaurant(Address address, Restaurant restaurant);
        public bool PostDish(Dish dish, int id);
        public bool DeleteRestaurant(int id);
        public bool DeleteDish(int id);
    }
}
