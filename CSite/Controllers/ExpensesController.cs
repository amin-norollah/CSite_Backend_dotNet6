using CSite.DTO;
using CSite.Helpers;
using CSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ControllerHelper _controllerHelper;

        public ExpensesController(ControllerHelper controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Getting items
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expenses>>> GetAll([FromQuery] int pageIndex = 1, int pageSize = 20)
        {
            return await _controllerHelper.GetAll<Expenses, Expenses>(pageIndex, pageSize);
        }

        /// <summary>
        /// Geting an item by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Expenses>> GetbyId(int id)
        {
            var result = await _controllerHelper.GetById<Expenses, Expenses>(predicate: x => x.ID == id);

            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Updating item
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Expenses expenses)
        {
            var result = await _controllerHelper.Update<Expenses, Expenses>(expenses, predicate: x => x.ID == id);

            if (result)
                return NoContent();
            else return BadRequest();
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Expenses>> PostItem(Expenses expenses)
        {
            var result = await _controllerHelper.Create<Expenses, Expenses>(expenses);

            return CreatedAtAction("GetbyId", new { id = result.ID }, result);
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _controllerHelper.Remove<Expenses>(id, predicate: x => x.ID == id);

            if (result)
                return NoContent();
            else return BadRequest();
        }
    }
}

