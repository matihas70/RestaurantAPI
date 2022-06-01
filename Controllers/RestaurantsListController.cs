using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            RestaurantsDataBase.connectAndGet();

            string result = _service.Get();
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] object restaurantJSON)
        {

            string restaurantstring = restaurantJSON.ToString();

            Restaurant restaurant = JsonConvert.DeserializeObject<Restaurant>(restaurantstring);

            Address address = restaurant.address;

            string result = _service.Post(address, restaurant);

            return Ok(result);
        }
    }
}
