using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PeerLandingFE.DTO.Req;

namespace PeerLandingFE.Controllers.api
{
    public class ApiLoginController:Controller
    {
        private readonly HttpClient _httpClient;

        public ApiLoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] ReqLoginDto loginRequest)
        {
            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5131/rest/v1/user/Login",content);

            if (response.IsSuccessStatusCode)
            {
                var responseData=await response.Content.ReadAsStringAsync();  
                return Ok(responseData);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
