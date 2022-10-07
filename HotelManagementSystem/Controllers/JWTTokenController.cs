using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JWTTokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public readonly HMSApiDbcontext _context;
        public JWTTokenController(IConfiguration configuration, HMSApiDbcontext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("OwnerToken")]
        public async Task<IActionResult> PostOwner(Owner owner)
        {
            if (owner != null && owner.Owner_Id != null && owner.Owner_Password != null)
            {
                var userData = _context.Owners.FirstOrDefault(u => u.Owner_Id == owner.Owner_Id && u.Owner_Password == owner.Owner_Password);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

                if (userData != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", owner.Owner_Id.ToString()),
                        new Claim("Password", owner.Owner_Password)

                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                       jwt.Issuer,
                       jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn
                    );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }

        [HttpPost]
        [Route("EmployeeToken")]
        public async Task<IActionResult> PostEmployee(int EmployeeId,string EmployeePassword)
        {
            if (EmployeeId != null && EmployeePassword != null)
            {
                var userData = _context.Employees.FirstOrDefault(u => u.Employee_Id == EmployeeId && u.Employee_Password == EmployeePassword);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

                if (userData != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", EmployeeId.ToString()),
                        new Claim("Password", EmployeePassword)

                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                       jwt.Issuer,
                       jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn
                    );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }
    }
}