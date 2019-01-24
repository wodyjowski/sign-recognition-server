using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignRecognition.Models;
using SignRecognition.Models.DBContext;
using SignRecognition.Models.FormModels;

namespace SignRecognition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        public PredictionController(UserManager<User> userManager, ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        // GET: api/Prediction
        [HttpGet]
        public async Task<IEnumerable<Prediction>> Get([FromQuery] int page = 0, [FromQuery] int amount = 20)
        {
            var user = await _userManager.GetUserAsync(User);
            user = new User();

            if (!ModelState.IsValid || user == null)
            {
                return null;
            }

            return _appDbContext.Predictions.OrderByDescending(p => p.CreationDate).Skip(amount * page).Take(amount);
        }

        [HttpGet("All")]
        public async Task<IEnumerable<Prediction>> GetAll([FromQuery] int page = 0, [FromQuery] int amount = 20)
        {
            return _appDbContext.Predictions.OrderByDescending(p => p.CreationDate).Skip(amount * page).Take(amount);
        }

        // POST: api/Prediction
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LocationViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            user = new User();

            if (!ModelState.IsValid || model == null || user == null)
            {
                return BadRequest();
            }

            Prediction pred = new Prediction()
            {
                CreationDate = DateTime.Now,
                User = null,
                Class = model.Class,
                Latitude = model.Latitude,
                Longitude = model.Longitude
            };
            _appDbContext.Add(pred);

            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/Prediction/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
