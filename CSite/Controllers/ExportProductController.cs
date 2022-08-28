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
    public class ExportProductController : ExportProductControllerGeneric<ExportProducts, ExportProductsDTO>
    {
        public ExportProductController(ControllerHelper _controllerHelper) : base(_controllerHelper) { }
    }

    [Authorize]
    public class ExportProductControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : ExportProducts
        where TEntityDTO : ExportProductsDTO
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
        [HttpPut("{id}/{ReceiptId}")]
        public async Task<IActionResult> PutItem(int id, int ReceiptId, TEntityDTO TEntityDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _controllerHelper.Update<TEntity, TEntityDTO>(
                TEntityDTO,
                include: source => source.Include(y => y.Product).Include(y => y.ExportReciept),
                predicate: x => x.ProductId == id && x.ReceiptId == ReceiptId
                );

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostItem(TEntityDTO[] TEntityDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _controllerHelper.Create<TEntity, TEntityDTO>(TEntityDTO);

            return NoContent();
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}/{ReceiptId}")]
        public async Task<IActionResult> DeleteItem(int id, int ReceiptId)
        {
            var result = await _controllerHelper.Remove<TEntity>(
                id,
                include: source => source.Include(y => y.Product).Include(y => y.ExportReciept),
                predicate: x => x.ProductId == id && x.ReceiptId == ReceiptId);

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }
    }
}


