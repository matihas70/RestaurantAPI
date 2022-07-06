using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI
{
    [ApiController]
    [Route("restaurant")]
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

            if(restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }


        [HttpPost]
        public ActionResult AddRestaurant([FromBody] AddRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isUpdated = _service.UpdateAddress(id, dto);

            if (isUpdated)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRestaurant([FromRoute] int id)
        {
            bool isDeleted = _service.DeleteRestaurant(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
