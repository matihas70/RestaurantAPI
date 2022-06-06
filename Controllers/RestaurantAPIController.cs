using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RestaurantAPI
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantAPIController : Controller
    {
        private readonly ILogger<RestaurantAPIController> _logger;
        private readonly IRestaurantsListService _service;

        public RestaurantAPIController(ILogger<RestaurantAPIController> logger, IRestaurantsListService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {

            return Ok(_service.Get());
        }

        [HttpGet("restaurants")]
        public ActionResult<IEnumerable<Restaurant>> Get2()
        {
            return Ok(_service.Get2());
        }

        [HttpPost("addRestaurant")]
        public ActionResult<string> Post([FromBody] object restaurantJSON)
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

            string result = _service.Post(address, restaurant);

            return Ok(result);
        }

        [HttpDelete("removeRestaurant")]
        public ActionResult<string> Delete([FromQuery] int id)
        {
            return Ok(_service.Delete(id));
        }
    }
}
