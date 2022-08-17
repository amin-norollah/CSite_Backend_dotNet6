using AutoMapper;
using CSite.DbContexts;
using CSite.DTO;
using CSite.Helpers;
using CSite.Models;
using CSite.Shared.Interfaces;
using CSite.Structures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class ExportRecieptController : ExportRecieptControllerGeneric<ExportReciept, ExportRecieptDTO>
    {
        public ExportRecieptController(
            IUnitOfWork<CSiteDbContext> unitOfWork,
            IMapper mapper,
            ControllerHelper _controllerHelper) : base(unitOfWork, mapper, _controllerHelper) { }
    }

    public class ExportRecieptControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : ExportReciept
        where TEntityDTO : ExportRecieptDTO
    {
        private readonly ControllerHelper _controllerHelper;
        private readonly IUnitOfWork<CSiteDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public ExportRecieptControllerGeneric(
            IUnitOfWork<CSiteDbContext> unitOfWork,
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, TEntityDTO TEntityDTO)
        {
            var result = await _controllerHelper.Update<TEntity, TEntityDTO>(
                TEntityDTO,
                include: source => source.Include(y => y.Car).Include(y => y.User).Include(y => y.Customer),
                predicate: x => x.ID == id
                );

            if (result)
                return NoContent();
            else return BadRequest();
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TEntity>> PostTEntity(TEntityDTO TEntityDTO)
        {
            //mapping
            var TEntity = _mapper.Map<TEntity>(TEntityDTO);

            //adding transaction
            Transactions transactions = new Transactions();
            if (TEntity.CarID == null)
            {
                var customer = await _unitOfWork.GetRepository<Customer>().GetFirstOrDefaultAsync(predicate: x => x.ID == TEntityDTO.customerID);

                if (customer == null)
                    return NotFound($"There is no customer with id '{TEntityDTO.customerID}'");

                transactions = new Transactions()
                {
                    AccountID = TEntity.CustomerID,
                    AccountType = (int)AccountType.Customer,
                    Amount = TEntity.Remaining,
                    Type = (int)TransType.Get,
                    Date = TEntity.Date,
                    OperationID = TEntity.ID,
                    Operation = (int)Operation.ExportReciept,
                    UserName = TEntity.UserName,
                };
                customer.Account += TEntity.Remaining;

                _unitOfWork.GetRepository<Customer>().Update(customer);
            }
            else
            {
                var car = await _unitOfWork.GetRepository<Car>().GetFirstOrDefaultAsync(predicate: x => x.ID == TEntityDTO.CarID);

                transactions = new Transactions()
                {
                    AccountID = TEntity.CarID,
                    AccountType = (int)AccountType.Car,
                    Amount = TEntity.Remaining,
                    Type = (int)TransType.Get,
                    Date = TEntity.Date,
                    OperationID = TEntity.ID,
                    Operation = (int)Operation.ExportReciept,
                    UserName = TEntity.UserName,
                };
                car.Account += TEntity.Remaining;

                _unitOfWork.GetRepository<Car>().Update(car);
            }

            _unitOfWork.GetRepository<TEntity>().Insert(TEntity);
            _unitOfWork.GetRepository<Transactions>().Insert(transactions);
            await _unitOfWork.SaveChangesAsync();

            //
            foreach (var item in TEntityDTO.Products)
            {
                //
                var target = _mapper.Map<ExportProduct>(item);
                target.ReceiptID = TEntity.ID;
                _unitOfWork.GetRepository<ExportProduct>().Insert(target);

                //
                var product = await _unitOfWork.GetRepository<Product>().GetFirstOrDefaultAsync(predicate: x => x.ID == target.ProductID);
                product.Quantity -= item.Quantity;
                _unitOfWork.GetRepository<Product>().Update(product);

                //
                if (TEntity.CarID != null)
                {
                    if (await _unitOfWork.GetRepository<CarProduct>().AnyAsync(w => w.ProductID == target.ProductID))
                    {
                        CarProduct car = await _unitOfWork.GetRepository<CarProduct>().GetFirstOrDefaultAsync(w => w.ProductID == item.ProductID);
                        car.Quantity += item.Quantity;
                        _unitOfWork.GetRepository<CarProduct>().Update(car);
                    }
                    else
                        _unitOfWork.GetRepository<CarProduct>().Insert(new CarProduct()
                        {
                            CarID = TEntity.CarID,
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                        });
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction("GetbyId", new { id = TEntity.ID }, _mapper.Map<TEntityDTO>(TEntity));
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTEntity(int id)
        {
            var result = await _controllerHelper.Remove<TEntity>(id, predicate: x => x.ID == id);

            if (result)
                return NoContent();
            else return BadRequest();
        }
    }
}
