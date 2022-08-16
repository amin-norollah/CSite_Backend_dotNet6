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
    public class ImportProductController : ControllerBase
    {
        private readonly ControllerHelper _controllerHelper;

        public ImportProductController(ControllerHelper controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Getting items
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportProductDTO>>> GetAll([FromQuery] int pageIndex = 1, int pageSize = 20)
        {
            return await _controllerHelper.GetAll<ImportProduct, ImportProductDTO>(pageIndex, pageSize);
        }

        /// <summary>
        /// Geting an item by id
        /// </summary>
        [HttpGet("{id}/{ReceiptID}")]
        public async Task<ActionResult<ImportProductDTO>> GetbyId(int id, int ReceiptID)
        {
            var result = await _controllerHelper.GetById<ImportProduct, ImportProductDTO>(predicate: x => x.ProductID == id && x.ReceiptID == ReceiptID);

            if (result == null)
                return NotFound();
            return Ok(result);
        }


        /// <summary>
        /// Updating item
        /// </summary>
        [HttpPut("{id}/{ReceiptID}")]
        public async Task<IActionResult> PutImportProduct(int id, int ReceiptID, ImportProductDTO importProductDTO)
        {
            var result = await _controllerHelper.Update<ImportProduct, ImportProductDTO>(
                importProductDTO,
                include: source => source.Include(y => y.Product).Include(y => y.ImportReciept),
                predicate: x => x.ProductID == id && x.ReceiptID == ReceiptID
                );

            if (result)
                return NoContent();
            else return BadRequest();
        }

        // POST: api/ImportProduct
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImportProduct>> PostImportProduct(ImportProductDTO[] importProductDTO)
        {
            await _controllerHelper.Create<ImportProduct, ImportProductDTO>(importProductDTO);

            return NoContent();
        }


        // DELETE: api/ImportProduct/5

        [HttpDelete("{id}/{ReceiptID}")]
        public async Task<IActionResult> DeleteImportProduct(int id, int ReceiptID)
        {
            var result = await _controllerHelper.Remove<ImportProduct>(id, predicate: w => w.ProductID == id && w.ReceiptID == ReceiptID);

            if (result)
                return NoContent();
            else return BadRequest();
        }
    }
}
