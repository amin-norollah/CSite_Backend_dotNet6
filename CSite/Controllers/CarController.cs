using CSite.DTO;
using CSite.Helpers;
using CSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class CarController : CarControllerGeneric<Car, CarDTO>
    {
        public CarController(ControllerHelper _controllerHelper) : base(_controllerHelper) { }
    }

    public class CarControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : Car
        where TEntityDTO : CarDTO
    {
        private readonly ControllerHelper _controllerHelper;

        public CarControllerGeneric(ControllerHelper controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Getting items
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<TEntityDTO>>> GetAll([FromQuery] int pageIndex = 1, int pageSize = 20)
        {
            return await _controllerHelper.GetAll<TEntity, TEntityDTO>(pageIndex, pageSize);
        }

        /// <summary>
        /// Geting an item by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntityDTO>> GetbyId(int id)
        {
            var result = await _controllerHelper.GetById<TEntity, TEntityDTO>(predicate: x => x.ID == id);

            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Updating item
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, TEntityDTO carDTO)
        {
            var result = await _controllerHelper.Update<TEntity, TEntityDTO>(carDTO, predicate: x => x.ID == id);

            if (result)
                return NoContent();
            else return BadRequest();
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TEntity>> PostItem(TEntityDTO carDTO)
        {
            var result = await _controllerHelper.Create<TEntity, TEntityDTO>(carDTO);

            return CreatedAtAction("GetbyId", new { id = result.ID }, result);
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _controllerHelper.Remove<TEntity>(id, predicate: x => x.ID == id);

            if (result)
                return NoContent();
            else return BadRequest();
        }
    }
}
