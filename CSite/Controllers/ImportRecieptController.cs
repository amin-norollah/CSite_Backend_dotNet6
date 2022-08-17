using AutoMapper;
using CSite.DbContexts;
using CSite.DTO;
using CSite.Helpers;
using CSite.Models;
using CSite.Shared.Interfaces;
using CSite.Structures;
using Microsoft.AspNetCore.Mvc;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class ImportRecieptController : ImportRecieptControllerGeneric<ImportReciept, ImportRecieptDTO>
    {
        public ImportRecieptController(
            IUnitOfWork<CSiteDbContext> unitOfWork,
            IMapper mapper,
            ControllerHelper _controllerHelper) : base(unitOfWork, mapper, _controllerHelper) { }
    }

    public class ImportRecieptControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : ImportReciept
        where TEntityDTO : ImportRecieptDTO
    {
        private readonly ControllerHelper _controllerHelper;
        private readonly IUnitOfWork<CSiteDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public ImportRecieptControllerGeneric(
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
        public async Task<IActionResult> PutTEntity(int id, TEntityDTO TEntityDTO)
        {
            var result = await _controllerHelper.Update<TEntity, TEntityDTO>(TEntityDTO, predicate: x => x.ID == id);

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
            //map
            var TEntity = _mapper.Map<TEntity>(TEntityDTO);

            Transactions tr = new Transactions()
            {
                AccountID = TEntity.SupplierID,
                AccountType = (int)AccountType.Supplier,
                Amount = TEntity.Remaining,
                Type = (int)TransType.Paid,
                Date = TEntity.Date,
                OperationID = TEntity.ID,
                Operation = (int)Operation.ImportReciept,
                UserName = TEntity.UserName,

            };
            await _unitOfWork.GetRepository<Transactions>().InsertAsync(tr);
            await _unitOfWork.GetRepository<TEntity>().InsertAsync(TEntity);

            var sup = await _unitOfWork.GetRepository<Supplier>().FindAsync(TEntity.SupplierID);
            sup.Account += TEntity.Remaining;
            _unitOfWork.GetRepository<Supplier>().Update(sup);

            await _unitOfWork.SaveChangesAsync();

            //
            foreach (var item in TEntityDTO.importProducts)
            {
                item.ImportReceiptID = TEntity.ID;
                await _unitOfWork.GetRepository<ImportProduct>().InsertAsync(_mapper.Map<ImportProduct>(item));

                var product = await _unitOfWork.GetRepository<Product>().FindAsync(item.ProductID);
                product.Quantity += item.Quantity;
                _unitOfWork.GetRepository<Product>().Update(product);
            }
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetbyId", new { id = TEntity.ID }, _mapper.Map<ImportProductDTO>(TEntity));
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTEntity(int id)
        {
            var result = await _controllerHelper.Remove<ImportProduct>(id, predicate: x => x.ID == id);

            if (result)
                return NoContent();
            else return BadRequest();
        }
    }
}
