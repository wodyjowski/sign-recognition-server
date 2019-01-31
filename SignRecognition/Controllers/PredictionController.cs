using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("{id}"), AllowAnonymous]
        public Prediction Get(string id)
        {
            var pred = _appDbContext.Find<Prediction>(id);

            return pred;
        }


        // GET: api/Prediction
        [HttpGet, AllowAnonymous]
        public IEnumerable<Prediction> Get([FromQuery] int page = 0, [FromQuery] int amount = 20, [FromQuery] string locationName = null)
        {
            var pred = _appDbContext.Predictions.AsQueryable();

            if (locationName != null)
            {
                pred = pred.Where(p => p.LocationName.Contains(locationName));
            }

            return pred.OrderByDescending(p => p.CreationDate).Skip(amount * page).Take(amount);
        }

        [HttpGet("UAll")]
        public async Task<IActionResult> GetUAll([FromQuery] int page = 0, [FromQuery] int amount = 20, [FromQuery] string locationName = null)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest();
            }

            var pred = _appDbContext.Predictions.Include(p => p.User).Where(p => p.User == user).AsQueryable();

            if (pred != null && locationName != null)
            {
                pred = pred.Where(p => p.LocationName.Contains(locationName));
            }

            return Ok(pred.OrderByDescending(p => p.CreationDate).Skip(amount * page).Take(amount));
        }

        [HttpGet("PredCount"), AllowAnonymous]
        public int PredCount([FromQuery] string locationName = null)
        {
            var pred = _appDbContext.Predictions.AsQueryable();

            if(locationName != null)
            {
                pred = pred.Where(p => p.LocationName.Contains(locationName));
            }

            return pred.Count();
        }

        [HttpGet("UPredCount")]
        public async Task<IActionResult> UserPredCount([FromQuery] string locationName = null)
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return BadRequest();
            }

            var pred = _appDbContext.Predictions.Include(p => p.User).Where(p => p.User == user)?.AsQueryable();

            if (pred != null && locationName != null)
            {
                pred = pred.Where(p => p.LocationName.Contains(locationName));
            }

            return Ok(pred?.Count() ?? 0);
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

        // method used by mobile app
        [HttpPost("PostToken"), AllowAnonymous]
        public async Task<IActionResult> PostToken([FromBody] TokenLocationFormModel model)
        {
            var token = _appDbContext.AppTokens.Where(t => t.Id == model.Token).Include(t => t.User).FirstOrDefault();
            var user = token?.User;

            if (!ModelState.IsValid || model == null)
            {
                return BadRequest();
            }

            if (user == null)
            {
                return StatusCode(401);
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
                User = user,
                Class = model.Class,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                LocationName = googleResponse?.results?.FirstOrDefault()?.formatted_address ?? "Unknown"
            };


            _appDbContext.Add(pred);

            await _appDbContext.SaveChangesAsync();

            connectLocation(pred);

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


        private void connectLocation(Prediction pred)
        {
            //var nearLocation = _appDbContext.Locations.Where(l => calculate(l.Latitude, l.Longitude, pred.Latitude, pred.Longitude) < 0.01).FirstOrDefault();

            var nearLocation = _appDbContext.Locations.Where(l => l.Class == pred.Class)
                .Select(l => new { location = l, distance = Math.Abs(l.Latitude-pred.Latitude) + Math.Abs(l.Longitude - pred.Longitude) }).OrderBy(x => x.distance).FirstOrDefault();



            if (nearLocation != null && calculateDistance(nearLocation.location, pred) > 0.01)
            {
                nearLocation = null;
            }

            if (nearLocation == null)
            {
                Location loc = new Location()
                {
                    Class = pred.Class,
                    Latitude = pred.Latitude,
                    Longitude = pred.Longitude,
                    CreationDate = DateTime.Now
                };
                _appDbContext.Add(loc);
                pred.Location = loc;
                _appDbContext.SaveChanges();
            }
            else
            {
                var location = nearLocation.location;
                pred.Location = location;
                _appDbContext.SaveChanges();

                var lat = _appDbContext.Predictions.Where(p => p.Location == location).Average(p => p.Latitude);
                var lng = _appDbContext.Predictions.Where(p => p.Location == location).Average(p => p.Longitude);

                location.Latitude = lat;
                location.Longitude = lng;

                _appDbContext.SaveChanges();
            }
        }

        private double calculateDistance(Location location1, Prediction location2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = (Math.PI / 180)*(location2.Latitude - location1.Latitude);  // deg2rad below
            var dLon = (Math.PI / 180)*(location2.Longitude - location1.Longitude);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos((Math.PI / 180)*(location1.Latitude)) * Math.Cos((Math.PI / 180)*(location2.Latitude)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }


    }
}
