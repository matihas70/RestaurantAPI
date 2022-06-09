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

        public IEnumerable<Restaurant> GetRestaurants()
        {
            return RestaurantsDataBase.GetRestaurantsList();
        }

        public IEnumerable<Dish> GetDishes(int id)
        {
            return RestaurantsDataBase.connectAndGetDishes(id);
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
            return RestaurantsDataBase.connectAndDelete(id, "restaurant");
        }
        public string DeleteDish(int id)
        {
            return RestaurantsDataBase.connectAndDelete(id, "dish");
        }
    }
}
