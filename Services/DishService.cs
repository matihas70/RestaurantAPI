using System.Data;
using System.Data.SqlClient;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Outputs;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        public IEnumerable<DishDto> GetAllDishes(int restaurantId);
        public int AddDish(int restaurantId, AddDishDto dto);
        public void UpdatePrice(int id, UpdatePriceDto dto);
        public void DeleteDish(int id);
    }

    public class DishService : IDishService
    {
        private readonly IConfiguration _config;

        public DishService(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<DishDto> GetAllDishes(int restaurantId)
        {
            DataTable DTable = new DataTable();

            using(SqlConnection connection = new SqlConnection(_config.GetConnectionString("DevConnection")))
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("getAllDishes", connection);

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("restaurantId", restaurantId);

                adapter.Fill(DTable);
            }
            if(DTable.Rows.Count == 0)
            {
                throw new NotFoundException("This restaurant hasn't any dish");
            }

            List<DishDto> dishes = new List<DishDto>();

            foreach(DataRow row in DTable.Rows)
            {
                dishes.Add(new DishDto()
                {
                    id = Convert.ToInt32(row["dish_id"]),
                    name = (string)row["name"],
                    type = (string)row["type"],
                    price = Convert.ToDecimal(row["price"])
                });
            }

            return dishes;
        }

        public int AddDish(int restaurantId, AddDishDto dto)
        {
            int id;

            using(SqlConnection connection = new SqlConnection(_config.GetConnectionString("DevConnection")))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("addDish", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("name", dto.name);
                cmd.Parameters.AddWithValue("type", dto.type);
                cmd.Parameters.AddWithValue("price", dto.price);
                cmd.Parameters.AddWithValue("restaurantId", restaurantId);

                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();

                id = (int)cmd.Parameters["@id"].Value;
            }

            if(id == (int)AddDishOutput.RestaurantNotFound)
            {
                throw new NotFoundException("Restaurant not found");
            }
            else if(id == (int)AddDishOutput.DishAlreadyExist)
            {
                throw new AlreadyExistException("Dish with this name already exist");
            }

            return id;
        }

        public void UpdatePrice(int id, UpdatePriceDto dto)
        {
            bool isUpdated;

            using(SqlConnection connection = new SqlConnection(_config.GetConnectionString("DevConnection")))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("updatePrice", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("price", dto.price);

                cmd.Parameters.Add("@isUpdated", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                isUpdated = Convert.ToBoolean(cmd.Parameters["@isUpdated"].Value);
            }

            if(!isUpdated)
            {
                throw new NotFoundException("Dish not found");
            }

        }

        public void DeleteDish(int id)
        {
            bool isDeleted;

            using(SqlConnection connection = new SqlConnection(_config.GetConnectionString("DevConnection")))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("deleteDish", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id", id);

                cmd.Parameters.Add("@isDeleted", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                isDeleted = Convert.ToBoolean(cmd.Parameters["@isDeleted"].Value);
            }

            if (!isDeleted)
            {
                throw new NotFoundException("Dish not found");
            }
        }
    }
}
