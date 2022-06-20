using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RestaurantAPI
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantAPIController : Controller
    {
        private readonly IRestaurantsListService _service;

        public RestaurantAPIController(IRestaurantsListService service)
        {
            _service = service;
        }


        [HttpGet]
        public ActionResult<string> Get()
        {

            return Ok(_service.Get());
        }


        [HttpGet("restaurants")]
        public ActionResult<IEnumerable<Restaurant>> GetRestaurants()
        {
            return Ok(_service.GetRestaurants());
        }


        [HttpGet("dishes")]
        public ActionResult<IEnumerable<Dish>> GetDishes([FromQuery]int id)
        {
            var dishes = _service.GetDishes(id);

            if(dishes.Count() == 0)
            {
                return NotFound();
            }

            return Ok(dishes);
        }


        [HttpPost("addRestaurant")]
        public ActionResult<string> PostRestaurant([FromBody] object restaurantJSON)
        {

            string restaurantstring = restaurantJSON.ToString();

            Restaurant restaurant = JsonConvert.DeserializeObject<Restaurant>(restaurantstring);

            Address address = restaurant.address;

            if(address.city == null || address.street == null || address.postal_code == null)
            {
                return BadRequest("Podaj poprawny adres");
            }

            if (restaurant.name == null || restaurant.category == null || restaurant.email == null)
            {
                return BadRequest("Podaj poprawne dane restauracji");
            }

            bool isCreated = _service.PostRestaurant(address, restaurant);

            if (isCreated)
            {
                return Ok("Utworzono restaurancję");
            }

            return BadRequest("Restauracja o tym adresie już istnieje");
        }


        [HttpPost("addDish")]
        public ActionResult<string> PostDish([FromBody] object dishJSON, [FromQuery] int id)
        {
            Dish dish = JsonConvert.DeserializeObject<Dish>(dishJSON.ToString());

            if(dish.name == null || dish.type == null || dish.price == 0)
            {
                return BadRequest("Podaj poprawne informacje o potrawie");
            }
            else if(id == 0)
            {
                return BadRequest("Podaj poprawne id Restauracji");
            }
            bool isAdded = _service.PostDish(dish, id);

            if (isAdded)
            {
                return Ok();
            }

            return BadRequest();
        }


        [HttpDelete("removeRestaurant")]
        public ActionResult<string> DeleteRestaurant([FromQuery] int id)
        {
            bool isDeleted = _service.DeleteRestaurant(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }


        [HttpDelete("removeDish")]
        public ActionResult<string> DeleteDish([FromQuery] int id)
        {
            bool isDeleted = _service.DeleteDish(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
