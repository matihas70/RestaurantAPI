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

        public string Delete(int id)
        {
            return RestaurantsDataBase.connectAndDelete(id);
        }
    }
}
