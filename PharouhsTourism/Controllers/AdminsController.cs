using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PharouhsTourism.Application.DTOs;
using PharouhsTourism.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PharouhsTourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public AdminsController(UserManager<IdentityUser> userManager,
            IConfiguration config,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _config = config;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            IdentityUser userFromDB = await _userManager.FindByNameAsync(dto.UserName);
            if (userFromDB != null)
            {
                bool found = await _userManager.CheckPasswordAsync(userFromDB, dto.Password);
                if (found)
                {
                    // generate token
                    List<Claim> userClaims = new();

                    userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDB.Id));
                    userClaims.Add(new Claim(ClaimTypes.Name, userFromDB.UserName));

                    var userRoles = await _userManager.GetRolesAsync(userFromDB);

                    foreach (var role in userRoles)
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:securityKey"]));

                    SigningCredentials signingCredentials = new(signInKey, SecurityAlgorithms.HmacSha256);

                    // create token
                    JwtSecurityToken token = new(
                        audience: _config["JWT:audience"],
                        issuer: _config["JWT:issuer"],
                        expires: DateTime.Now.AddMinutes(30),
                        claims: userClaims,
                        signingCredentials: signingCredentials
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = DateTime.Now.AddMinutes(30)
                    });
                }
                ModelState.AddModelError("UserName", "Username or password Invalid");
            }
            return BadRequest("this Admin account is not exist.");
        }

    }
}
