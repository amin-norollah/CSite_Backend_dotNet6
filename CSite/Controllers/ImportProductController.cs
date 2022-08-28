using CSite.DTO;
using CSite.Helpers;
using CSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class ImportProductController : ImportProductControllerGeneric<ImportProducts, ImportProductsDTO>
    {
        public ImportProductController(ControllerHelper _controllerHelper) : base(_controllerHelper) { }
    }

    [Authorize]
    public class ImportProductControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : ImportProducts
        where TEntityDTO : ImportProductsDTO
    {
        private readonly ControllerHelper _controllerHelper;

        public ImportProductControllerGeneric(ControllerHelper controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Getting items
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntityDTO>>> GetAll([FromQuery] int pageIndex = 1, int pageSize = 20)
        {
            return Ok(await _controllerHelper.GetAll<TEntity, TEntityDTO>(pageIndex, pageSize));
        }

        /// <summary>
        /// Geting an item by id
        /// </summary>
        [HttpGet("{id}/{ReceiptId}")]
        public async Task<ActionResult<TEntityDTO>> GetbyId(int id, int ReceiptId)
        {
            var result = await _controllerHelper.GetById<TEntity, TEntityDTO>(predicate: x => x.ProductId == id && x.ReceiptId == ReceiptId);

            if (result == null)
                return NotFound($"There is no item with ID '{id}'");
            return Ok(result);
        }


        /// <summary>
        /// Updating item
        /// </summary>
        [HttpPut("{id}/{ReceiptId}")]
        public async Task<IActionResult> PutTEntity(int id, int ReceiptId, TEntityDTO TEntityDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _controllerHelper.Update<TEntity, TEntityDTO>(
                TEntityDTO,
                include: source => source.Include(y => y.Product).Include(y => y.Receipt),
                predicate: x => x.ProductId == id && x.ReceiptId == ReceiptId
                );

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }

        // POST: api/TEntity
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TEntity>> PostTEntity(TEntityDTO[] TEntityDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _controllerHelper.Create<TEntity, TEntityDTO>(TEntityDTO);

            return NoContent();
        }


        // DELETE: api/TEntity/5

        [HttpDelete("{id}/{ReceiptId}")]
        public async Task<IActionResult> DeleteTEntity(int id, int ReceiptId)
        {
            var result = await _controllerHelper.Remove<TEntity>(id, predicate: w => w.ProductId == id && w.ReceiptId == ReceiptId);

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }
    }
}
