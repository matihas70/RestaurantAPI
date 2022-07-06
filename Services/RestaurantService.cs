using System.Data;
using System.Data.SqlClient;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        public IEnumerable<RestaurantDto> GetAllRestaurants();
        public RestaurantDto GetRestaurant(int id);
        public int AddRestaurant(AddRestaurantDto dto);
        public bool UpdateAddress(int id, UpdateAddressDto dto);
        public bool DeleteRestaurant(int id);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly IConfiguration _config;
        public RestaurantService(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<RestaurantDto> GetAllRestaurants()
        {
            DataTable DTable = new DataTable();

            using (SqlConnection connection= new SqlConnection(_config.GetConnectionString("DevConnection")))
            {
                connection.Open();
                
                SqlDataAdapter adapter = new SqlDataAdapter("getAllRestaurants", connection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(DTable);
            }

            List<RestaurantDto> restaurants = new List<RestaurantDto>();

            foreach(DataRow row in DTable.Rows)
            {
                restaurants.Add(new RestaurantDto
                {
                    id = Convert.ToInt32(row["restaurant_id"]),
                    name = (string)row["name"],
                    category = (string)row["category"],
                    city = (string)row["city"],
                    street = (string)row["street"],
                    postal_code = (string)row["postal_code"]
                }); 
            }
            return restaurants;
        }

        public RestaurantDto GetRestaurant(int id)
        {
            DataTable DTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DevConnection")))
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("getRestaurant", connection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(DTable);
            }

            if(DTable.Rows.Count > 0)
            {
                RestaurantDto restaurant = new RestaurantDto()
                {
                    id = Convert.ToInt32(DTable.Rows[0]["restaurant_id"]),
                    name = (string)DTable.Rows[0]["name"],
                    category = (string)DTable.Rows[0]["category"],
                    city = (string)DTable.Rows[0]["city"],
                    street = (string)DTable.Rows[0]["street"],
                    postal_code = (string)DTable.Rows[0]["postal_code"]
                };

                return restaurant;
            }

            return null;
        }

        public int AddRestaurant(AddRestaurantDto dto)
        {
            int id;

            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DevConnection")))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("addRestaurant", connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("name", dto.name);
                cmd.Parameters.AddWithValue("category", dto.category);
                cmd.Parameters.AddWithValue("email", dto.email);
                cmd.Parameters.AddWithValue("city", dto.city);
                cmd.Parameters.AddWithValue("street", dto.street);
                cmd.Parameters.AddWithValue("postal_code", dto.postal_code);

                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();

                id = (int)cmd.Parameters["@id"].Value;
            }

            return id;
        }

        public bool UpdateAddress(int id, UpdateAddressDto dto)
        {
            bool isUpdated;

            using(SqlConnection connection = new SqlConnection(_config.GetConnectionString("DevConnection")))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("updateAddress", connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("city", dto.city);
                cmd.Parameters.AddWithValue("street", dto.street);
                cmd.Parameters.AddWithValue("postal_code", dto.postal_code);

                cmd.Parameters.Add("@isUpdated", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();

                isUpdated = Convert.ToBoolean(cmd.Parameters["@isUpdated"].Value);
            }

            return isUpdated;
        }

        public bool DeleteRestaurant(int id)
        {
            bool isDeleted;

            using(SqlConnection connection = new SqlConnection(_config.GetConnectionString("DevConnection")))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("deleteRestaurant", connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);

                cmd.Parameters.Add("@isDeleted", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();

                isDeleted = Convert.ToBoolean(cmd.Parameters["@isDeleted"].Value);
            }

            return isDeleted;
        }
    }
}
