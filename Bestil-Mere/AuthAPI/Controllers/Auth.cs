using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : Controller
    {
        private JwtHandler _jwt;
        private ICollection<User> _users;

        public Auth(JwtHandler jwt)
        {
            _jwt = jwt;
            
            /*
            _users = new List<User>()
            {
                new User() { Id = 0, Role = "User", Username = "pleaseuse@this.dot"},
                new User() { Id = 1, Role = "User", Username = "customer@mail.com", Scopes = new []{"customer"}},
                new User() { Id = 2, Role = "Admin", Username = "javascript@is.king", Scopes = new []{"restaurant"}},
                new User() { Id = 3, Role = "User", Username = "test@example.com", Scopes = new []{"customer", "restaurant"}},
            };
            */
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] ApiLoginModel model)
        {
            var client = new RestClient("http://customerapi/api/customers");
            var request = new RestRequest($"email/{model.Username}");
            
            var response = client.Get<User>(request);
//            var user = _users.FirstOrDefault(x => x.Username == model.Username);
            
            var user = response.Data;
            
            if (!response.IsSuccessful) // TODO: Implement proper authentication in v2
            {
                return Unauthorized("Could not login with the provided credentials");
            }

            var jwt = _jwt.GenerateAccessToken(user);
            return Ok(new ApiJwtModel() { access_token = jwt.Item1, expires_in = jwt.Item2});
        }
    }
}