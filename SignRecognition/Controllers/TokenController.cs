using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignRecognition.Models;
using SignRecognition.Models.DBContext;

namespace SignRecognition.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TokenController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<User> _userManager;


        public TokenController(ApplicationDbContext appDbContext, UserManager<User> userManager)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        // GET: api/Token
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = _appDbContext.Users.Where(u => u.Id == id).Include(u => u.Tokens).FirstOrDefault();

            if (user == null) return BadRequest();

            var currentUser = await _userManager.GetUserAsync(User);
            var adminRole = (await _userManager.GetRolesAsync(currentUser)).Any(r => r == "Admin");

            if (user != currentUser && !adminRole) return BadRequest();


            return Ok(user.Tokens);
        }



        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var token = _appDbContext.AppTokens.Where(t => t.Id == id).Include(t => t.User).FirstOrDefault();

            if (token == null) return BadRequest();

            var currentUser = await _userManager.GetUserAsync(User);
            var adminRole = (await _userManager.GetRolesAsync(currentUser)).Any(r => r == "Admin");

            if (token.User != currentUser && !adminRole) return BadRequest();

            _appDbContext.Remove(token);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
