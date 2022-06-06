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

        public string Post(Address address, Restaurant restaurant)
        {
           return RestaurantsDataBase.connectAndPost(address, restaurant);
        }

        public string Delete(int id)
        {
            return RestaurantsDataBase.connectAndDelete(id);
        }
    }
}
