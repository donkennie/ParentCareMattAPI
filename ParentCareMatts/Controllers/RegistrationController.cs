using AuthenticationPlugin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ParentCareMatts.Data;
using ParentCareMatts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ParentCareMatts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegistrationController : ControllerBase
    {
        private PCMDbContext _dbContext;
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        public RegistrationController(PCMDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _auth = new AuthService(_configuration);
        }

        [HttpPost]
        public IActionResult Register([FromBody] Registration user)
        {
            var userWithTheSameEmail = _dbContext.GetRegistrations.Where(u => u.Email == user.Email).SingleOrDefault();
            if (userWithTheSameEmail != null)
            {
                return BadRequest("User with the same email already exist");
            }

            var userObj = new Registration
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Password = SecurePasswordHasherHelper.Hash(user.Password),
                ConfirmPassword = SecurePasswordHasherHelper.Hash(user.ConfirmPassword)


            };

            _dbContext.GetRegistrations.Add(userObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }
       

      
        
    }
}
