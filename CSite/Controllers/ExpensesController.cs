using CSite.Helpers;
using CSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class ExpensesController : ExpensesControllerGeneric<Expenses>
    {
        public ExpensesController(ControllerHelper _controllerHelper) : base(_controllerHelper) { }
    }

    [Authorize]
    public class ExpensesControllerGeneric<TEntity> : ControllerBase
        where TEntity : Expenses
    {
        private readonly ControllerHelper _controllerHelper;

        public ExpensesControllerGeneric(ControllerHelper controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Getting items
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<TEntity>>> GetAll([FromQuery] int pageIndex = 1, int pageSize = 20)
        {
            return Ok(await _controllerHelper.GetAll<TEntity, TEntity>(pageIndex, pageSize));
        }

        /// <summary>
        /// Geting an item by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> GetbyId(int id)
        {
            var result = await _controllerHelper.GetById<TEntity, TEntity>(predicate: x => x.Id == id);

            if (result == null)
                return NotFound($"There is no item with ID '{id}'");
            return Ok(result);
        }

        /// <summary>
        /// Updating item
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, TEntity expenses)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _controllerHelper.Update<TEntity, TEntity>(expenses, predicate: x => x.Id == id);

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostItem(TEntity expenses)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _controllerHelper.Create<TEntity, TEntity>(expenses);

            return CreatedAtAction("GetbyId", new { id = result.Id }, new { Id = result.Id });
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _controllerHelper.Remove<TEntity>(id, predicate: x => x.Id == id);

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }
    }
}

