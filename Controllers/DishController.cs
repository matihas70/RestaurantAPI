using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Services;
using RestaurantAPI.Models;
using RestaurantAPI.Outputs;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("restaurant{restaurantId}/dish")]
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

            if(dishes == null)
            {
                return NotFound();
            }

            return Ok(dishes);
        }

        [HttpPost]
        public ActionResult AddDish([FromRoute]int restaurantId, [FromBody] AddDishDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = _service.AddDish(restaurantId, dto);

            if(id == (int)AddDishOutput.RestaurantNotFound)
            {
                return NotFound();
            }
            else if(id == (int)AddDishOutput.DishAlreadyExist)
            {
                return BadRequest();
            }

            return Created($"restaurant/{restaurantId}/dish/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePrice([FromRoute] int id, [FromBody] UpdatePriceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isUpdated = _service.UpdatePrice(id, dto);

            if (isUpdated)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDish([FromRoute] int id)
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
