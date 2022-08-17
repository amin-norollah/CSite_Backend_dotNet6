using CSite.DTO;
using CSite.Helpers;
using CSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class ImportProductController : ImportProductControllerGeneric<ImportProduct, ImportProductDTO>
    {
        public ImportProductController(ControllerHelper _controllerHelper) : base(_controllerHelper) { }
    }

    public class ImportProductControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : ImportProduct
        where TEntityDTO : ImportProductDTO
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
        [HttpGet("{id}/{ReceiptID}")]
        public async Task<ActionResult<TEntityDTO>> GetbyId(int id, int ReceiptID)
        {
            var result = await _controllerHelper.GetById<TEntity, TEntityDTO>(predicate: x => x.ProductID == id && x.ReceiptID == ReceiptID);

            if (result == null)
                return NotFound();
            return Ok(result);
        }


        /// <summary>
        /// Updating item
        /// </summary>
        [HttpPut("{id}/{ReceiptID}")]
        public async Task<IActionResult> PutTEntity(int id, int ReceiptID, TEntityDTO TEntityDTO)
        {
            var result = await _controllerHelper.Update<TEntity, TEntityDTO>(
                TEntityDTO,
                include: source => source.Include(y => y.Product).Include(y => y.ImportReciept),
                predicate: x => x.ProductID == id && x.ReceiptID == ReceiptID
                );

            if (result)
                return NoContent();
            else return BadRequest();
        }

        // POST: api/TEntity
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TEntity>> PostTEntity(TEntityDTO[] TEntityDTO)
        {
            await _controllerHelper.Create<TEntity, TEntityDTO>(TEntityDTO);

            return NoContent();
        }


        // DELETE: api/TEntity/5

        [HttpDelete("{id}/{ReceiptID}")]
        public async Task<IActionResult> DeleteTEntity(int id, int ReceiptID)
        {
            var result = await _controllerHelper.Remove<TEntity>(id, predicate: w => w.ProductID == id && w.ReceiptID == ReceiptID);

            if (result)
                return NoContent();
            else return BadRequest();
        }
    }
}
