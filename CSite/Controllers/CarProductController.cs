using AutoMapper;
using CSite.DbContexts;
using CSite.DTO;
using CSite.Models;
using CSite.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class CarProductController : ControllerBase
    {
        private readonly IUnitOfWork<CSiteDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public CarProductController(
            IUnitOfWork<CSiteDbContext> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Getting all carproducts
        /// </summary>
        /// <param name="carID"></param>
        /// <returns></returns>
        [HttpGet("{carID}")]
        public async Task<ActionResult<IEnumerable<CarProductDTO>>> GetCarProducts(int carID)
        {
            var carProducts = await _unitOfWork.GetRepository<CarProduct>().GetAsync(
                include: source => source.Include(y => y.Car).Include(y => y.Product),
                predicate: x => x.CarID == carID
                );

            return Ok(carProducts.Select(item => new CarProductDTO()
            {
                CarID = item.CarID,
                Quantity = item.Quantity,
                ProductID = item.ProductID,
                ProductName = item.Product.Name
            }));
        }

        /// <summary>
        /// Getting an carproduct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carID"></param>
        /// <returns></returns>
        [HttpGet("{id}/{carID}")]
        public async Task<ActionResult<CarProductDTO>> GetCarProduct(int id, int carID)
        {
            var carProduct = await _unitOfWork.GetRepository<CarProduct>().GetFirstOrDefaultAsync(
                include: source => source.Include(y => y.Car).Include(y => y.Product),
                predicate: x => x.CarID == carID && x.ProductID == id
                );

            if (carProduct == null)
                return NotFound();

            return Ok(new CarProductDTO()
            {
                CarID = carProduct.CarID,
                Quantity = carProduct.Quantity,
                ProductID = carProduct.ProductID,
                ProductName = carProduct.Product.Name
            });
        }

        /// <summary>
        /// Updating carproduct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carID"></param>
        /// <param name="carProductDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}/{carID}")]
        public async Task<IActionResult> PutCarProduct(int id, int carID, CarProductDTO carProductDTO)
        {
            var targetProduct = await _unitOfWork.GetRepository<CarProduct>().GetFirstOrDefaultAsync(
                include: source => source.Include(y => y.Car).Include(y => y.Product),
                predicate: x => x.ProductID == id && x.CarID == carID
                );

            //guard: if CarProduct is not exist
            if (targetProduct == null)
                return BadRequest();

            //update
            _unitOfWork.GetRepository<CarProduct>().Update(_mapper.Map<CarProduct>(carProductDTO));
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post new carProduct
        /// </summary>
        /// <param name="carProductDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostCarProduct(CarProductDTO[] carProductDTO)
        {
            foreach (var item in carProductDTO)
            {
                var carProduct = _mapper.Map<CarProduct>(item);
                await _unitOfWork.GetRepository<CarProduct>().InsertAsync(carProduct);
                await _unitOfWork.SaveChangesAsync();
            }

            return NoContent();
        }

        /// <summary>
        /// Deleting the carProduct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carID"></param>
        /// <returns></returns>
        [HttpDelete("{id}/{carID}")]
        public async Task<IActionResult> DeleteCarProduct(int id, int carID)
        {
            var targetCar = await _unitOfWork.GetRepository<CarProduct>().GetFirstOrDefaultAsync(
                include: source => source.Include(y => y.Car).Include(y => y.Product),
                predicate: x => x.ProductID == id && x.CarID == carID);

            //guard: if car is not exist
            if (targetCar == null)
                return BadRequest();

            //delete
            await _unitOfWork.GetRepository<CarProduct>().DeleteAsync(targetCar.ID);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
