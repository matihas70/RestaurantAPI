using System.Data.SqlClient;
namespace RestaurantAPI
{
    static public class RestaurantsDataBase
    {
        private const string ConnectionString = "Server = localhost\\SQLEXPRESS;Database = RestaurantsDB;Trusted_Connection=True;";
        
        static public void connect()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                Console.WriteLine("CONNECTED");
            }
        }
    }
}
