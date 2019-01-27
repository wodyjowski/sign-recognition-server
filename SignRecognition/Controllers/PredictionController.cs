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
            return _appDbContext.Predictions.OrderByDescending(p => p.CreationDate).Skip(amount * page).Take(amount);
        }

        [HttpGet("UAll")]
        public async Task<IActionResult> GetUAll([FromQuery] int page = 0, [FromQuery] int amount = 20)
        {
            var user = await _userManager.GetUserAsync(User);
            user = new User();

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user.Predictions);
        }

        [HttpGet("PredCount")]
        public int PredCount()
        {
            return _appDbContext.Predictions.Count();
        }

        [HttpGet("UPredCount")]
        public async Task<IActionResult> UserPredCount()
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return BadRequest();
            }

            return Ok(user.Predictions?.Count() ?? 0);
        }

        // POST: api/Prediction
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LocationFormModel model)
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

        [HttpPost("Image")]
        public async Task<IActionResult> PostImage([FromBody] LocationImageFormModel model)
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


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                BadRequest();
            }

            Prediction prediction = _appDbContext.Find<Prediction>(id);

            bool acceptDelete = (await _userManager.GetRolesAsync(user)).Any(r => r == "Admin") || prediction?.User == user;
  

            if (prediction != null && acceptDelete)
            {
                _appDbContext.Remove(prediction);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
