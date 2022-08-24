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
    public class ImportRecieptController : ImportRecieptControllerGeneric<ImportReciepts, ImportRecieptsDTO>
    {
        public ImportRecieptController(
            IUnitOfWork<CSiteDBContext> unitOfWork,
            IMapper mapper,
            ControllerHelper _controllerHelper) : base(unitOfWork, mapper, _controllerHelper) { }
    }

    public class ImportRecieptControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : ImportReciepts
        where TEntityDTO : ImportRecieptsDTO
    {
        private readonly ControllerHelper _controllerHelper;
        private readonly IUnitOfWork<CSiteDBContext> _unitOfWork;
        private readonly IMapper _mapper;

        public ImportRecieptControllerGeneric(
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
            return await _controllerHelper.GetAll<TEntity, TEntityDTO>(pageIndex, pageSize);
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
        public async Task<IActionResult> PutTEntity(int id, TEntityDTO TEntityDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _controllerHelper.Update<TEntity, TEntityDTO>(TEntityDTO, predicate: x => x.Id == id);

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }

        /// <summary>
        /// Posting new item
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TEntity>> PostTEntity(TEntityDTO TEntityDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //map
            var TEntity = _mapper.Map<TEntity>(TEntityDTO);

            Transactions tr = new Transactions()
            {
                AccountId = TEntity.SupplierId,
                AccountType = (int)AccountType.Supplier,
                Amount = TEntity.Remaining,
                Type = (int)TransType.Paid,
                Date = TEntity.Date,
                OperationId = TEntity.Id,
                Operation = (int)Operation.ImportReciept,
                UserId = TEntity.UserId?? 0,

            };
            await _unitOfWork.GetRepository<Transactions>().InsertAsync(tr);
            await _unitOfWork.GetRepository<TEntity>().InsertAsync(TEntity);

            var sup = await _unitOfWork.GetRepository<Suppliers>().FindAsync(TEntity.SupplierId);
            sup.Account += TEntity.Remaining;
            _unitOfWork.GetRepository<Suppliers>().Update(sup);

            await _unitOfWork.SaveChangesAsync();

            //
            foreach (var item in TEntityDTO.importProducts)
            {
                item.Id = TEntity.Id;
                await _unitOfWork.GetRepository<ImportProducts>().InsertAsync(_mapper.Map<ImportProducts>(item));

                var product = await _unitOfWork.GetRepository<Products>().FindAsync(item.Product.Id);
                product.Quantity += item.Quantity;
                _unitOfWork.GetRepository<Products>().Update(product);
            }
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetbyId", new { id = TEntity.Id }, _mapper.Map<ImportProductsDTO>(TEntity));
        }

        /// <summary>
        /// Deleting the existing item
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTEntity(int id)
        {
            var result = await _controllerHelper.Remove<ImportProducts>(id, predicate: x => x.Id == id);

            if (result)
                return NoContent();
            else return BadRequest($"There is no item with ID '{id}'");
        }
    }
}
