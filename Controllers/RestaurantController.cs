using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI
{
    [Route("restaurant")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            var restaurants = _service.GetAllRestaurants();

            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetRestaurant([FromRoute]int id)
        {
            var restaurant = _service.GetRestaurant(id);

            return Ok(restaurant);
        }


        [HttpPost]
        public ActionResult AddRestaurant([FromBody] AddRestaurantDto dto)
        {
            int id = _service.AddRestaurant(dto);

            if(id == 0)
            {
                return BadRequest();
            }
            return Created($"restaurant/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAddress([FromRoute] int id, [FromBody] UpdateAddressDto dto)
        {
            _service.UpdateAddress(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRestaurant([FromRoute] int id)
        {
            _service.DeleteRestaurant(id);

            return NoContent();

        }
    }
}
