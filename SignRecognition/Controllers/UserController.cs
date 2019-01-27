using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignRecognition.Models;
using SignRecognition.Models.DBContext;
using SignRecognition.Models.ViewModels;

namespace SignRecognition.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        public UserController(UserManager<User> userManager, ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }



        // GET: api/User
        [HttpGet]
        public async Task<UserViewModel> Get()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            return new UserViewModel()
            {
                UserName = currentUser.UserName,
                // Send true if user is admin
                AdminRights = (await _userManager.GetRolesAsync(currentUser)).Any(r => r == "Admin")
            };
        }

        [HttpGet("Data")]
        public async Task<UserDataViewModel> GetData()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            return new UserDataViewModel()
            {
                UserName = currentUser.UserName,
                CreationDate = currentUser.CreationDate,
                Email = currentUser.Email,
                AdminRights = (await _userManager.GetRolesAsync(currentUser)).Any(r => r == "Admin")
            };
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
