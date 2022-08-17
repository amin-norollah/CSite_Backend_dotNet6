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
    public class ExportProductController : ExportProductControllerGeneric<ExportProduct, ExportProductDTO>
    {
        public ExportProductController(ControllerHelper _controllerHelper) : base(_controllerHelper) { }
    }

    public class ExportProductControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : ExportProduct
        where TEntityDTO : ExportProductDTO
    {
        private readonly ControllerHelper _controllerHelper;

        public ExportProductControllerGeneric(ControllerHelper controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Getting items
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntityDTO>>> GetAll([FromQuery] int pageIndex = 1, int pageSize = 20)
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
        [HttpPut("{id}/{ReceiptID}")]
        public async Task<IActionResult> PutItem(int id, int ReceiptID, TEntityDTO TEntityDTO)
        {
            var result = await _controllerHelper.Update<TEntity, TEntityDTO>(
                TEntityDTO,
                include: source => source.Include(y => y.Product).Include(y => y.ExportReciept),
                predicate: x => x.ProductID == id && x.ReceiptID == ReceiptID
                );

            if (result)
                return NoContent();
            else return BadRequest();
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostItem(TEntityDTO[] TEntityDTO)
        {
            await _controllerHelper.Create<TEntity, TEntityDTO>(TEntityDTO);

            return NoContent();
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}/{ReceiptID}")]
        public async Task<IActionResult> DeleteItem(int id, int ReceiptID)
        {
            var result = await _controllerHelper.Remove<TEntity>(
                id,
                include: source => source.Include(y => y.Product).Include(y => y.ExportReciept),
                predicate: x => x.ProductID == id && x.ReceiptID == ReceiptID);

            if (result)
                return NoContent();
            else return BadRequest();
        }
    }
}


