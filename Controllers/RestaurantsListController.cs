using Microsoft.AspNetCore.Mvc;

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
            RestaurantsDataBase.connect();

            string result = _service.Get();
            return Ok(result);
        }
    }
}
