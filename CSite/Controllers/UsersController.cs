using AutoMapper;
using CSite.Data.DdContexts;
using CSite.Data.DTO;
using CSite.Data.Entities;
using CSite.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : UsersControllerGeneric<AspNetUsers, UsersDTO>
    {
        public UsersController(
            IUnitOfWork<CSiteDBContext> unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }

    public class UsersControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : AspNetUsers
        where TEntityDTO : UsersDTO
    {
        private readonly IUnitOfWork<CSiteDBContext> _unitOfWork;
        private readonly IMapper _mapper;

        public UsersControllerGeneric(
            IUnitOfWork<CSiteDBContext> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Getting list of users
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntityDTO>>> GetUsers([FromQuery] int pageIndex = 1, int pageSize = 20)
        {
            var users = (await _unitOfWork.GetRepository<TEntity>().GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize)).ToList();
            return _mapper.Map<List<TEntityDTO>>(users);
        }

        /// <summary>
        /// Geting an user by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntityDTO>> GetbyId(string id)
        {
            var result = await _unitOfWork.GetRepository<TEntity>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);

            if (result == null)
                return NotFound($"There is no item with ID '{id}'");
            return Ok(_mapper.Map<TEntityDTO>(result));
        }

        /// <summary>
        /// Geting an users by id (only for first 100 users)
        /// </summary>
        [HttpGet("AsList")]
        public async Task<ActionResult<TEntityDTO>> GetbyIdAsList([FromQuery] List<string> ids)
        {
            //it is a demo, so the 100 users are fine
            var users = (await _unitOfWork.GetRepository<TEntity>().GetPagedListAsync(pageIndex: 1, pageSize: 100)).ToList();
            var reqs = ids.Distinct().ToList();//remove duplicate items

            var results = users.Where(x => reqs.Contains(x.Id));

            return Ok(_mapper.Map<List<TEntityDTO>>(results));
        }
    }
}
