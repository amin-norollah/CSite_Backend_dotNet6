using AutoMapper;
using CSite.DbContexts;
using CSite.DTO;
using CSite.Models;
using CSite.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CSite.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : UsersControllerGeneric<Users, UsersDTO>
    {
        public UsersController(
            IUnitOfWork<CSiteDbContext> unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }

    public class UsersControllerGeneric<TEntity, TEntityDTO> : ControllerBase
        where TEntity : Users
        where TEntityDTO : UsersDTO
    {
        private readonly IUnitOfWork<CSiteDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public UsersControllerGeneric(
            IUnitOfWork<CSiteDbContext> unitOfWork,
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
            return _mapper.Map<List<TEntityDTO>>(await _unitOfWork.GetRepository<TEntity>().GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize));
        }

        ///// <summary>
        ///// Getting an user by id
        ///// </summary>
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TEntityDTO>> GetUsers(string id)
        //{
        //    await _unitOfWork.GetRepository<TEntity>().GetFirstOrDefaultAsync();
        //    var users = await _context.Users.FindAsync(id);

        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return users.UsersToDTO();
        //}

        //// PUT: api/Users/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUsers(string id, UsersDTO usersDTO)
        //{
        //    Users users = usersDTO.DTOToUsers();
        //    if (id != users.UserName)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(users).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UsersExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Users
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Users>> PostUsers(UsersDTO usersDTO)
        //{
        //    Users users = usersDTO.DTOToUsers();

        //    _context.Users.Add(users);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (UsersExists(users.UserName))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetUsers", new { id = users.UserName }, users);
        //}

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUsers(string id)
        //{
        //    var users = await _context.Users.FindAsync(id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(users);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool UsersExists(string id)
        //{
        //    return _context.Users.Any(e => e.UserName == id);
        //}
    }
}
