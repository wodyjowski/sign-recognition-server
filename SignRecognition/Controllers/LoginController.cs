using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SignRecognition.Authentication;
using SignRecognition.Models;
using SignRecognition.Models.DBContext;
using SignRecognition.Models.FormModels;
using SignRecognition.Models.TokenIssuer;

namespace SignRecognition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly JsonSerializerSettings _serializerSettings;

        private IConfiguration _config;



        public LoginController(UserManager<User> userManager, IJwtFactory jwtFactory, ApplicationDbContext appDbContext, IOptions<JwtIssuerOptions> jwtOptions, IConfiguration config)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser([FromBody]RegistrationFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User()
            {
                UserName = model.Login,
                Email = model.Email,
                CreationDate = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            _userManager.AddToRoleAsync(user, "User").Wait();

            if (!result.Succeeded) return StatusCode(208, result);

            await _appDbContext.SaveChangesAsync();

            return StatusCode(208, result);
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]CredentialsFormModel login)
        {
            IActionResult response = BadRequest("login failed");
            var user = await AuthenticateAsync(login);

            var roles = await _userManager.GetRolesAsync(user);

            if (user != null)
            {
                var tokenString = _jwtFactory.GenerateEncodedToken(user, roles);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private async Task<User> AuthenticateAsync(CredentialsFormModel login)
        {
            User user = await _userManager.FindByNameAsync(login.UserName);

            if (user != null)
            {
                // check the credentials  
                if (await _userManager.CheckPasswordAsync(user, login.Password))
                {
                    return user;
                }
            }

            return null;
        }
    }
}