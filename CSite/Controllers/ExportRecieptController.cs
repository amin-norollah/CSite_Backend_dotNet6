using AutoMapper;
using CSite.DbContexts;
using CSite.DTO;
using CSite.DTO.Extension_Methods;
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
    public class ExportRecieptController : ControllerBase
    {
        private readonly ControllerHelper _controllerHelper;
        private readonly IUnitOfWork<CSiteDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public ExportRecieptController(
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
        public async Task<ActionResult<IEnumerable<ExportRecieptDTO>>> GetAll([FromQuery] int pageIndex = 1, int pageSize = 20)
        {
            return await _controllerHelper.GetAll<ExportReciept, ExportRecieptDTO>(pageIndex, pageSize);
        }

        /// <summary>
        /// Geting an item by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExportRecieptDTO>> GetbyId(int id)
        {
            var result = await _controllerHelper.GetById<ExportReciept, ExportRecieptDTO>(predicate: x => x.ID == id);

            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Updating item
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, ExportRecieptDTO exportRecieptDTO)
        {
            var result = await _controllerHelper.Update<ExportReciept, ExportRecieptDTO>(
                exportRecieptDTO,
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
        public async Task<ActionResult<ExportReciept>> PostExportReciept(ExportRecieptDTO exportRecieptDTO)
        {
            //mapping
            var exportReciept = _mapper.Map<ExportReciept>(exportRecieptDTO);

            //adding transaction
            Transactions transactions = new Transactions();
            if (exportReciept.CarID == null)
            {
                var customer = await _unitOfWork.GetRepository<Customer>().GetFirstOrDefaultAsync(predicate: x => x.ID == exportRecieptDTO.customerID);

                if (customer == null)
                    return NotFound($"There is no customer with id '{exportRecieptDTO.customerID}'");

                transactions = new Transactions()
                {
                    AccountID = exportReciept.CustomerID,
                    AccountType = (int)AccountType.Customer,
                    Amount = exportReciept.Remaining,
                    Type = (int)TransType.Get,
                    Date = exportReciept.Date,
                    OperationID = exportReciept.ID,
                    Operation = (int)Operation.ExportReciept,
                    UserName = exportReciept.UserName,
                };
                customer.Account += exportReciept.Remaining;

                _unitOfWork.GetRepository<Customer>().Update(customer);
            }
            else
            {
                var car = await _unitOfWork.GetRepository<Car>().GetFirstOrDefaultAsync(predicate: x => x.ID == exportRecieptDTO.CarID);

                transactions = new Transactions()
                {
                    AccountID = exportReciept.CarID,
                    AccountType = (int)AccountType.Car,
                    Amount = exportReciept.Remaining,
                    Type = (int)TransType.Get,
                    Date = exportReciept.Date,
                    OperationID = exportReciept.ID,
                    Operation = (int)Operation.ExportReciept,
                    UserName = exportReciept.UserName,
                };
                car.Account += exportReciept.Remaining;

                _unitOfWork.GetRepository<Car>().Update(car);
            }

            _unitOfWork.GetRepository<ExportReciept>().Insert(exportReciept);
            _unitOfWork.GetRepository<Transactions>().Insert(transactions);
            await _unitOfWork.SaveChangesAsync();

            //
            foreach (var item in exportRecieptDTO.Products)
            {
                //
                var target = _mapper.Map<ExportProduct>(item);
                target.ReceiptID = exportReciept.ID;
                _unitOfWork.GetRepository<ExportProduct>().Insert(target);

                //
                var product = await _unitOfWork.GetRepository<Product>().GetFirstOrDefaultAsync(predicate: x => x.ID == target.ProductID);
                product.Quantity -= item.Quantity;
                _unitOfWork.GetRepository<Product>().Update(product);

                //
                if (exportReciept.CarID != null)
                {
                    if(await _unitOfWork.GetRepository<CarProduct>().AnyAsync(w => w.ProductID == target.ProductID))
                    {
                        CarProduct car = await _unitOfWork.GetRepository<CarProduct>().GetFirstOrDefaultAsync(w => w.ProductID == item.ProductID);
                        car.Quantity += item.Quantity;
                        _unitOfWork.GetRepository<CarProduct>().Update(car);
                    }
                    else
                        _unitOfWork.GetRepository<CarProduct>().Insert(new CarProduct()
                        {
                            CarID = exportReciept.CarID,
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                        });
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction("GetbyId", new { id = exportReciept.ID }, _mapper.Map<ExportRecieptDTO>(exportReciept));
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExportReciept(int id)
        {
            var result = await _controllerHelper.Remove<ExportReciept>(id, predicate: x => x.ID == id);

            if (result)
                return NoContent();
            else return BadRequest();
        }
    }
}
