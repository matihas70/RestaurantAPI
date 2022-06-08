using System.Data.SqlClient;


namespace RestaurantAPI
{
    public class RestaurantsListService : IRestaurantsListService
    {
        public string Get()
        {
            string result = "Hello user";
            return result;
        }

        public IEnumerable<Restaurant> Get2()
        {
            return RestaurantsDataBase.GetRestaurantsList();
        }

        public string PostRestaurant(Address address, Restaurant restaurant)
        {
           return RestaurantsDataBase.connectAndPostRestaurant(address, restaurant);
        }

        public string PostDish(Dish dish, int id)
        {
            return RestaurantsDataBase.connectAndPostDish(dish, id);
        }

        public string DeleteRestaurant(int id)
        {
            Restaurant restaurant = null;

            return RestaurantsDataBase.connectAndDelete<Restaurant>(id, restaurant);
        }
        public string DeleteDish(int id)
        {
            Dish dish = null;

            return RestaurantsDataBase.connectAndDelete<Dish>(id, dish);
        }
    }
}
