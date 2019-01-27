using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SignRecognition.Models;
using SignRecognition.Models.DBContext;
using SignRecognition.Models.FormModels;

namespace SignRecognition.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;



        public PredictionController(UserManager<User> userManager, ApplicationDbContext appDbContext, IConfiguration config, IHttpClientFactory clientFactory)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _config = config;
            _clientFactory = clientFactory;
        }

        // GET: api/Prediction
        [HttpGet, AllowAnonymous]
        public IEnumerable<Prediction> Get([FromQuery] int page = 0, [FromQuery] int amount = 20)
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

        [HttpGet("PredCount"), AllowAnonymous]
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
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] LocationFormModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            user = new User();

            if (!ModelState.IsValid || model == null || user == null)
            {
                return BadRequest();
            }

            // https://maps.google.com/maps/api/geocode/json?latlng=x,y&sensor=false&key=AIzaSyCFcgapJG17nwAFC0Yohs0x6Z9IsBwclq4

            var apiKey = _config["GoogleMaps:ApiKey"];
            var request = new HttpRequestMessage(HttpMethod.Get,
            $"https://maps.google.com/maps/api/geocode/json?latlng={model.Latitude.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}," +
            $"{model.Longitude.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}&sensor=false&key=AIzaSyCFcgapJG17nwAFC0Yohs0x6Z9IsBwclq4");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            var parsedJson = JObject.Parse(await response.Content.ReadAsStringAsync());

            var googleResponse = parsedJson.ToObject<GoogleFormModel>();

            Prediction pred = new Prediction()
            {
                CreationDate = DateTime.Now,
                User = null,
                Class = model.Class,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                LocationName = googleResponse?.results?.FirstOrDefault()?.formatted_address ?? "Unknown"
            };


            _appDbContext.Add(pred);

            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Image"), AllowAnonymous]
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
