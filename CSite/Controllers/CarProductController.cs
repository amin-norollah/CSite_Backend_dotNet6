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
    public class CarProductController : CarProductControllerGeneric<CarProduct, CarProductDTO>
    {
        public CarProductController(ControllerHelper _controllerHelper) : base(_controllerHelper) { }
    }

    public class CarProductControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : CarProduct
        where TEntityDTO : CarProductDTO
    {
        private readonly ControllerHelper _controllerHelper;

        public CarProductControllerGeneric(ControllerHelper controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Getting items by carID
        /// </summary>
        [HttpGet("{carID}")]
        public async Task<ActionResult<List<TEntityDTO>>> GetAll(int carID, [FromQuery] int pageIndex = 1, int pageSize = 20)
        {
            return Ok( await _controllerHelper.GetAll<TEntity, TEntityDTO>(
                pageIndex,
                pageSize,
                predicate: x => x.CarID == carID,
                include: source => source.Include(y => y.Car).Include(y => y.Product)
                ));
        }

        /// <summary>
        /// Geting an item by id and carID
        /// </summary>
        [HttpGet("{id}/{carID}")]
        public async Task<ActionResult<CarProductDTO>> GetCarProduct(int id, int carID)
        {
            var result = await _controllerHelper.GetById<TEntity, TEntityDTO>(
                include: source => source.Include(y => y.Car).Include(y => y.Product),
                predicate: x => x.CarID == carID && x.ProductID == id
                );

            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Updating item by carID
        /// </summary>
        [HttpPut("{id}/{carID}")]
        public async Task<IActionResult> PutItem(int id, int carID, TEntityDTO carProductDTO)
        {
            var result = await _controllerHelper.Update<TEntity, TEntityDTO>(
                carProductDTO,
                include: source => source.Include(y => y.Car).Include(y => y.Product),
                predicate: x => x.ProductID == id && x.CarID == carID
                );

            if (result)
                return NoContent();
            else return BadRequest();
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TEntity>> PostItem(TEntityDTO[] carProductDTO)
        {
            await _controllerHelper.Create<TEntity, TEntityDTO>(carProductDTO);

            return NoContent();
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}/{carID}")]
        public async Task<IActionResult> DeleteItem(int id, int carID)
        {
            var result = await _controllerHelper.Remove<TEntity>(
                id,
                include: source => source.Include(y => y.Car).Include(y => y.Product),
                predicate: x => x.ProductID == id && x.CarID == carID
                );

            if (result)
                return NoContent();
            else return BadRequest();
        }
    }
}
