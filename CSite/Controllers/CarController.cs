using CSite.DTO;
using CSite.Helpers;
using CSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ControllerHelper _controllerHelper;

        public CarController(ControllerHelper controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Getting items
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<CarDTO>>> GetAll([FromQuery] int pageIndex = 1, int pageSize = 20)
        {
            return await _controllerHelper.GetAll<Car, CarDTO>(pageIndex, pageSize);
        }

        /// <summary>
        /// Geting an item by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDTO>> GetbyId(int id)
        {
            var result = await _controllerHelper.GetById<Car, CarDTO>(predicate: x => x.ID == id);

            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Updating item
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, CarDTO carDTO)
        {
            var result = await _controllerHelper.Update<Car, CarDTO>(carDTO, predicate: x => x.ID == id);

            if(result)
                return NoContent();
            else return BadRequest();
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Car>> PostItem(CarDTO carDTO)
        {
            var result = await _controllerHelper.Create<Car, CarDTO>(carDTO);

            return CreatedAtAction("GetbyId", new { id = result.ID }, result);
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _controllerHelper.Remove<Car>(id, predicate: x => x.ID == id);

            if (result)
                return NoContent();
            else return BadRequest();
        }
    }
}
