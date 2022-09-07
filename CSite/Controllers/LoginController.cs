//using CSite.DbContexts;
//using CSite.DTO;
//using CSite.Entities;
//using CSite.Shared.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace CSite.Controllers
//{
//    [ApiVersion("1.0")]
//    [Route("api/{version:apiVersion}/[controller]")]
//    [ApiController]
//    public class LoginController : ControllerBase
//    {
//        private readonly IUnitOfWork<CSiteDBContext> _unitOfWork;

//        public LoginController(IUnitOfWork<CSiteDBContext> unitOfWork)

//        {
//            _unitOfWork = unitOfWork;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(Login userLogin)
//        {
//            var user = await _unitOfWork.GetRepository<Users>().GetFirstOrDefaultAsync(
//                predicate: x => x.Name == userLogin.userName
//                 && x.Password == userLogin.password
//                );

//            if (user != null)
//            {
//                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("s923r4jvJ-DSvsxoi8y-9vJDf6-832bnFV"));

//                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//                var data = new List<Claim>();
//                data.Add(new Claim("username", user.Name));
//                data.Add(new Claim("type", user.Type.ToString()));

//                var token = new JwtSecurityToken(
//                claims: data,
//                expires: DateTime.Now.AddMinutes(120),
//                signingCredentials: credentials);

//                return Ok(new
//                {
//                    token = new JwtSecurityTokenHandler().WriteToken(token),
//                    userName = user.Name,
//                    type = user.Type,
//                });
//            }
//            else
//            {
//                return Unauthorized();
//            }
//        }
//    }
//}
