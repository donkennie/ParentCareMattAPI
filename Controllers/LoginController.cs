using AuthenticationPlugin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParentCareMatts.Data;
using ParentCareMatts.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

namespace ParentCareMatts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private PCMDbContext _dbContext;
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        public LoginController(PCMDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _auth = new AuthService(_configuration);
        }

        [HttpPost]
        [Route("LoginUser")]
        public IActionResult LoginUser([FromBody] Login user)
        {
            var userEmail = _dbContext.GetLogins.SingleOrDefault(u => u.Email == user.Email);
            if (userEmail == null)
            {
                return NotFound();
            }

            if (!SecurePasswordHasherHelper.Verify(user.Password, userEmail.Password))
            {
                return Unauthorized();
            }
            

            var claims = new[]
            {   
               new Claim(JwtRegisteredClaimNames.Email, user.Email),
               new Claim(ClaimTypes.Email, user.Email),
            };
            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                user_id = userEmail.Id
            });




        }
    }
}
