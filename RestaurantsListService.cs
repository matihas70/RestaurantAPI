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

        public string Post(Address address, Restaurant restaurant)
        {
           return RestaurantsDataBase.connectAndPost(address, restaurant);
        }
    }
}
