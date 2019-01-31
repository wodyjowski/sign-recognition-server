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

namespace SignRecognition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {

        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<User> _userManager;


        public LocationController(UserManager<User> userManager, ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }


        // GET: api/Location
        [HttpGet, AllowAnonymous]
        public IEnumerable<Location> Get([FromQuery] int page = 0, [FromQuery] int amount = 20, [FromQuery] string locationName = null)
        {
            var loc = _appDbContext.Locations;

            return loc.OrderByDescending(p => p.CreationDate).Skip(amount * page).Take(amount);
        }


        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public IActionResult DeleteLocation(string id)
        {
            var locationToRemove = _appDbContext.Find<Location>(id);

            if (locationToRemove == null) return BadRequest();

            var predictions = _appDbContext.Predictions.Where(p => p.Location == locationToRemove);

            foreach (var pred in predictions)
            {
                _appDbContext.Remove(pred);
            }
            _appDbContext.Remove(locationToRemove);

            _appDbContext.SaveChanges();

            return Ok();
        }


    }
}
