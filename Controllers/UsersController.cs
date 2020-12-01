using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Admin5.Providers;
using Microsoft.Extensions.Options;
using GestionCommerciale.Models;
using Helpers;
using Providers;

namespace GestionCommerciale.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : SuperController<User>
    {
        private readonly AppSettings _appSettings;
        public UsersController(MyDbContext context, IOptions<AppSettings> appSettings) : base(context)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}")]
        public override async Task<IActionResult> GetAll(
            int startIndex,
            int pageSize,
            string sortBy,
            string sortDir
        )
        {

            var list = await _context.Users
                    .OrderByName<User>(sortBy, sortDir == "desc")   
                    .Skip(startIndex)
                    .Take(pageSize)
                    .Include(e => e.Role)
                    .ToListAsync()
            ;


            int count = await _context.Users.CountAsync();
           

            return Ok(new { list = list, count = count });
        }

        [HttpPost]
        public async Task<ActionResult<User>> LogIn(UserDTO model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Email ou mot pass sont vide");

            var user = await _context.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(x => x.Email == model.Email)
                ;

            Role role = null;
            // check if username exists
            if (user == null)
                return BadRequest("Email est pas correct");

            if (user.Password == model.Password)
            {
                role = await _context.Roles.FirstOrDefaultAsync(e => e.Id == user.IdRole);
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                // var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                // var key = Encoding.ASCII.GetBytes("this is the secret phrase");
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, role.Id.ToString())
                    };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var createToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(createToken);
                // var token = tokenHandler.ReadJwtToken(createToken);

                // remove password before returning
                user.Password = "";
                user.Role = role;
                return Ok(new { user, token, idRole = role.Id });
            }

            return BadRequest("Mot pass est pas correct");
        }

        [HttpGet]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> GetTokken()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return token;
        }

    }

    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}