using CSite.DTO;
using CSite.Helpers;
using CSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomerController : CustomerControllerGeneric<Customers, CustomersDTO>
    {
        public CustomerController(ControllerHelper _controllerHelper) : base(_controllerHelper) { }
    }

    public class CustomerControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : Customers
        where TEntityDTO : CustomersDTO
    {
        private readonly ControllerHelper _controllerHelper;

        public CustomerControllerGeneric(ControllerHelper controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Getting items
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<TEntityDTO>>> GetAll([FromQuery] int pageIndex = 1, int pageSize = 20)
        {
            return Ok(await _controllerHelper.GetAll<TEntity, TEntityDTO>(pageIndex, pageSize));
        }

        /// <summary>
        /// Geting an item by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntityDTO>> GetbyId(int id)
        {
            var result = await _controllerHelper.GetById<TEntity, TEntityDTO>(predicate: x => x.Id == id);

            if (result == null)
                return NotFound($"There is no item with ID '{id}'");
            return Ok(result);
        }

        /// <summary>
        /// Updating item
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, TEntityDTO customerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _controllerHelper.Update<TEntity, TEntityDTO>(customerDTO, predicate: x => x.Id == id);

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostItem(TEntityDTO customerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _controllerHelper.Create<TEntity, TEntityDTO>(customerDTO);

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
