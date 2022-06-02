using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestaurantAPI
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantsListController : Controller
    {
        private readonly ILogger<RestaurantsListController> _logger;
        private readonly IRestaurantsListService _service;

        public RestaurantsListController(ILogger<RestaurantsListController> logger, IRestaurantsListService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            string result = _service.Get();
            return Ok(result);
        }

        [HttpGet("restaurants")]
        public ActionResult<List<Restaurant>> Get2()
        {
            return Ok(_service.Get2());
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] JObject restaurantJSON)
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
    }
}
