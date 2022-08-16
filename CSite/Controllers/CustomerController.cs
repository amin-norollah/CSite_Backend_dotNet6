using CSite.DTO;
using CSite.Helpers;
using CSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ControllerHelper _controllerHelper;

        public CustomerController(ControllerHelper controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Getting items
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAll([FromQuery] int pageIndex = 1, int pageSize = 20)
        {
            return await _controllerHelper.GetAll<Customer, CustomerDTO>(pageIndex, pageSize);
        }

        /// <summary>
        /// Geting an item by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetbyId(int id)
        {
            var result = await _controllerHelper.GetById<Customer, CustomerDTO>(predicate: x => x.ID == id);

            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Updating item
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, CustomerDTO customerDTO)
        {
            var result = await _controllerHelper.Update<Customer, CustomerDTO>(customerDTO, predicate: x => x.ID == id);

            if (result)
                return NoContent();
            else return BadRequest();
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Customer>> PostItem(CustomerDTO customerDTO)
        {
            var result = await _controllerHelper.Create<Customer, CustomerDTO>(customerDTO);

            return CreatedAtAction("GetbyId", new { id = result.ID }, result);
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _controllerHelper.Remove<Customer>(id, predicate: x => x.ID == id);

            if (result)
                return NoContent();
            else return BadRequest();
        }
    }
}
