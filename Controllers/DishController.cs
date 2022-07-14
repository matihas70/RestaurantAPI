using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Services;
using RestaurantAPI.Models;
using RestaurantAPI.Outputs;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("restaurant/{restaurantId}/dish")]
    public class DishController : Controller
    {
        private readonly IDishService _service;

        public DishController(IDishService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DishDto>> GetAllDishes([FromRoute]int restaurantId)
        {
            var dishes = _service.GetAllDishes(restaurantId);

            return Ok(dishes);
        }

        [HttpPost]
        public ActionResult AddDish([FromRoute]int restaurantId, [FromBody] AddDishDto dto)
        {
            int id = _service.AddDish(restaurantId, dto);

            return Created($"restaurant/{restaurantId}/dish/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePrice([FromRoute] int id, [FromBody] UpdatePriceDto dto)
        {
            _service.UpdatePrice(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDish([FromRoute] int id)
        {
            _service.DeleteDish(id);

            return NoContent();
        }
    }
}
