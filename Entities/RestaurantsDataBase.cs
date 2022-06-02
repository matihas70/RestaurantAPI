using System.Data.SqlClient;
namespace RestaurantAPI
{
    static public class RestaurantsDataBase
    {
        private const string ConnectionString = "Server = localhost\\SQLEXPRESS;Database = RestaurantsDB;Trusted_Connection=True;";

        static public List<Restaurant> GetRestaurantsList()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                Console.WriteLine("CONNECTED");

                string sql = "SELECT Restaurants.restaurant_id, Restaurants.name, Restaurants.category, Restaurants.email, Addresses.address_id, Addresses.city, Addresses.street, Addresses.postal_code FROM Restaurants INNER JOIN Addresses ON Restaurants.address = Addresses.address_id";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Restaurant> restaurants = new List<Restaurant>();

                        while (reader.Read())
                        {
                            int restaurant_id = int.Parse(reader["restaurant_id"].ToString()),
                                address_id = int.Parse(reader["address_id"].ToString());

                            string name = reader["name"].ToString(),
                                   category = reader["category"].ToString(),
                                   email = reader["email"].ToString(),
                                   city = reader["city"].ToString(),
                                   street = reader["street"].ToString(),
                                   postal_code = reader["postal_code"].ToString();

                            restaurants.Add(new Restaurant(name, category, email,
                                            new Address(city, street, postal_code, address_id),
                                            restaurant_id));
                        }
                        return restaurants;
                    }
                }
            }
        }
        static public string connectAndPost(Address address, Restaurant restaurant)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                Console.WriteLine("CONNECTED");

                //Sprawdzam czy podany adres już istnieje
                string sql = $"SELECT * FROM Addresses WHERE city = '{address.city}' AND street = '{address.street}'";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return "Podany adres już istnieje";
                        }
                    }
                }


                sql = $"INSERT INTO Addresses VALUES ('{address.city}', '{address.street}', '{address.postal_code}')";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ResponseFromDataBase.LastAddressId = int.Parse(reader["id"].ToString());
                        }
                    }
                    
                }
                
                sql = $"INSERT INTO Restaurants VALUES ('{restaurant.name}', '{restaurant.category}', '{restaurant.email}', {ResponseFromDataBase.LastAddressId})";
                ResponseFromDataBase.LastAddressId = 0;

                using(SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using(var reader = cmd.ExecuteReader())
                    {
                        return "Dodano restauracje";
                    }
                }
            }
        }


    }
}
