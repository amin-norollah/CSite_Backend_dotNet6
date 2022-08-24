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
    public class CarProductController : CarProductControllerGeneric<CarProducts, CarProductsDTO>
    {
        public CarProductController(ControllerHelper _controllerHelper) : base(_controllerHelper) { }
    }

    public class CarProductControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : CarProducts
        where TEntityDTO : CarProductsDTO
    {
        private readonly ControllerHelper _controllerHelper;

        public CarProductControllerGeneric(ControllerHelper controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Getting items by carId
        /// </summary>
        [HttpGet("{carId}")]
        public async Task<ActionResult<List<TEntityDTO>>> GetAll(int carId, [FromQuery] int pageIndex = 1, int pageSize = 20)
        {
            return Ok( await _controllerHelper.GetAll<TEntity, TEntityDTO>(
                pageIndex,
                pageSize,
                predicate: x => x.CarId == carId,
                include: source => source.Include(y => y.Car).Include(y => y.Product)
                ));
        }

        /// <summary>
        /// Geting an item by id and carId
        /// </summary>
        [HttpGet("{id}/{carId}")]
        public async Task<ActionResult<CarProductsDTO>> GetCarProduct(int id, int carId)
        {
            var result = await _controllerHelper.GetById<TEntity, TEntityDTO>(
                include: source => source.Include(y => y.Car).Include(y => y.Product),
                predicate: x => x.CarId == carId && x.ProductId == id
                );

            if (result == null)
                return NotFound($"There is no item with ID '{id}'");
            return Ok(result);
        }

        /// <summary>
        /// Updating item by carId
        /// </summary>
        [HttpPut("{id}/{carId}")]
        public async Task<IActionResult> PutItem(int id, int carId, TEntityDTO carProductDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _controllerHelper.Update<TEntity, TEntityDTO>(
                carProductDTO,
                include: source => source.Include(y => y.Car).Include(y => y.Product),
                predicate: x => x.ProductId == id && x.CarId == carId
                );

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostItem(TEntityDTO[] carProductDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _controllerHelper.Create<TEntity, TEntityDTO>(carProductDTO);

            return NoContent();
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}/{carId}")]
        public async Task<IActionResult> DeleteItem(int id, int carId)
        {
            var result = await _controllerHelper.Remove<TEntity>(
                id,
                include: source => source.Include(y => y.Car).Include(y => y.Product),
                predicate: x => x.ProductId == id && x.CarId == carId
                );

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }
    }
}
