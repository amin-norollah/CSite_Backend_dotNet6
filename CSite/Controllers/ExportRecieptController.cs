using AutoMapper;
using CSite.DbContexts;
using CSite.DTO;
using CSite.Helpers;
using CSite.Models;
using CSite.Shared.Interfaces;
using CSite.Structures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class ExportRecieptController : ExportRecieptControllerGeneric<ExportReciepts, ExportRecieptsDTO>
    {
        public ExportRecieptController(
            IUnitOfWork<CSiteDBContext> unitOfWork,
            IMapper mapper,
            ControllerHelper _controllerHelper) : base(unitOfWork, mapper, _controllerHelper) { }
    }

    [Authorize]
    public class ExportRecieptControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : ExportReciepts
        where TEntityDTO : ExportRecieptsDTO
    {
        private readonly ControllerHelper _controllerHelper;
        private readonly IUnitOfWork<CSiteDBContext> _unitOfWork;
        private readonly IMapper _mapper;

        public ExportRecieptControllerGeneric(
            IUnitOfWork<CSiteDBContext> unitOfWork,
            IMapper mapper,
            ControllerHelper controllerHelper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, TEntityDTO TEntityDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _controllerHelper.Update<TEntity, TEntityDTO>(
                TEntityDTO,
                include: source => source.Include(y => y.Car).Include(y => y.User).Include(y => y.Customer),
                predicate: x => x.Id == id
                );

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TEntity>> PostTEntity(TEntityDTO exportRecieptDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //mapping
            var exportReciept = _mapper.Map<TEntity>(exportRecieptDTO);

            //adding transaction
            Transactions transactions = new Transactions();
            if (exportReciept.CarId == null)
            {
                var customer = await _unitOfWork.GetRepository<Customers>().GetFirstOrDefaultAsync(predicate: x => x.Id == exportRecieptDTO.CustomerId);

                if (customer == null)
                    return NotFound($"There is no customer with id '{exportRecieptDTO.CustomerId}'");

                transactions = new Transactions()
                {
                    AccountId = exportReciept.CustomerId,
                    AccountType = (int)AccountType.Customer,
                    Amount = exportReciept.Remaining,
                    Type = (int)TransType.Get,
                    Date = exportReciept.Date,
                    OperationId = exportReciept.Id,
                    Operation = (int)Operation.ExportReciept,
                    UserId = exportReciept.UserId ?? 0,
                };
                customer.Account += exportReciept.Remaining;

                _unitOfWork.GetRepository<Customers>().Update(customer);
            }
            else
            {
                var car = await _unitOfWork.GetRepository<Cars>().GetFirstOrDefaultAsync(predicate: x => x.Id == exportRecieptDTO.CarId);

                transactions = new Transactions()
                {
                    AccountId = exportReciept.CarId,
                    AccountType = (int)AccountType.Car,
                    Amount = exportReciept.Remaining,
                    Type = (int)TransType.Get,
                    Date = exportReciept.Date,
                    OperationId = exportReciept.Id,
                    Operation = (int)Operation.ExportReciept,
                    UserId = exportReciept.UserId ?? 0,
                };
                car.Account += exportReciept.Remaining;

                _unitOfWork.GetRepository<Cars>().Update(car);
            }

            _unitOfWork.GetRepository<TEntity>().Insert(exportReciept);
            _unitOfWork.GetRepository<Transactions>().Insert(transactions);
            await _unitOfWork.SaveChangesAsync();

            //
            foreach (var item in exportRecieptDTO.ExportProducts)
            {
                //
                var target = _mapper.Map<ExportProducts>(item);
                target.ReceiptId = exportReciept.Id;
                _unitOfWork.GetRepository<ExportProducts>().Insert(target);

                //
                var product = await _unitOfWork.GetRepository<Products>().GetFirstOrDefaultAsync(predicate: x => x.Id == target.ProductId);
                product.Quantity -= item.Quantity;
                _unitOfWork.GetRepository<Products>().Update(product);

                //
                if (exportReciept.CarId != null)
                {
                    if (await _unitOfWork.GetRepository<CarProducts>().AnyAsync(w => w.ProductId == target.ProductId))
                    {
                        CarProducts car = await _unitOfWork.GetRepository<CarProducts>().GetFirstOrDefaultAsync(w => w.ProductId == item.ProductId);
                        car.Quantity += item.Quantity;
                        _unitOfWork.GetRepository<CarProducts>().Update(car);
                    }
                    else
                        _unitOfWork.GetRepository<CarProducts>().Insert(new CarProducts()
                        {
                            CarId = exportReciept.CarId,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                        });
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction("GetbyId", new { id = exportReciept.Id }, _mapper.Map<TEntityDTO>(exportReciept));
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTEntity(int id)
        {
            var result = await _controllerHelper.Remove<TEntity>(id, predicate: x => x.Id == id);

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }
    }
}
