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

        static public string connectAndPostRestaurant(Address address, Restaurant restaurant)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                Console.WriteLine("CONNECTED");

                //checking if address already exist
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
                            ResponseFromDataBase.LastAddressId = int.Parse(reader["address_id"].ToString());
                        }
                    }

                }

                sql = $"INSERT INTO Restaurants VALUES ('{restaurant.name}', '{restaurant.category}', '{restaurant.email}', {ResponseFromDataBase.LastAddressId})";
                ResponseFromDataBase.LastAddressId = 0;

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        return "Dodano restauracje";
                    }
                }
            }
        }

        static public string connectAndPostDish(Dish dish, int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                Console.WriteLine("CONNECTED");

                string sql = $"SELECT name FROM Restaurants WHERE restaurant_id = {id}";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == false)
                        {
                            return "Nie ma restauracji o tym id";
                        }
                    }
                }

                sql = $"SELECT dish_id FROM Dishes WHERE name = '{dish.name}' AND restaurant = {id}";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return "W tej restauracji jest już ta potrawa";
                        }
                    }
                }

                int intiger = (int)dish.price;
                int dec = (int)(100 * (dish.price - intiger));

                sql = $"INSERT INTO Dishes VALUES('{dish.name}', '{dish.type}', {intiger}.{dec}, {id})";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        return "Dodano potrawę";
                    }
                }
            }
        }

        static public string connectAndDelete(int id, string toDelete)
        {
            toDelete = toDelete.ToLower();

            string[] describe = new string[4];

            if(toDelete == "restaurant")
            {
                describe = new string[4] { "Restaurants", "restaurant_id", "restauracji", "restaurację"};
            }
            else if (toDelete == "dish")
            {
                describe = new string[4] { "Dishes", "dish_id", "potrawy", "potrawę" };
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                Console.WriteLine("CONNECTED");

                string sql = $"SELECT name FROM {describe[0]} WHERE {describe[1]} = {id}";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == false)
                        {
                            return $"Nie ma {describe[2]} o tym id";
                        }
                    }
                }

                sql = $"DELETE {describe[0]} WHERE {describe[1]} = {id}";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        return $"Usunięto {describe[3]}";
                    }
                }
            }
        }
    }
}

