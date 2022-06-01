﻿using System.Data.SqlClient;
namespace RestaurantAPI
{
    static public class RestaurantsDataBase
    {
        private const string ConnectionString = "Server = localhost\\SQLEXPRESS;Database = RestaurantsDB;Trusted_Connection=True;";
        static private int maxAddressId { get; set; }
        static public void connectAndGet()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                Console.WriteLine("CONNECTED");

                string sql = "SELECT * FROM Restaurants";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                reader.Close();
                
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
