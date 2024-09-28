using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using PeerLandingFE.DTO.Req;
using PeerLandingFE.DTO.Res;
using PeerLandingFE.Models;

namespace PeerLandingFE.Controllers.api
{
	public class ApiMstUserController : Controller
	{
		private readonly HttpClient _httpClient;

		public ApiMstUserController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var token = Request.Headers["Authorization"].ToString().Replace("Bearer ","");
			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.GetAsync("http://localhost:5131/rest/v1/user/GetAllUsers");

			if (response.IsSuccessStatusCode)
			{
				var responseData = await response.Content.ReadAsStringAsync();
				return Ok(responseData);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetUserById(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return BadRequest("User ID cannot null or empty");
			}

			var token = Request.Headers["Authorization"].ToString().Replace("Bearer ","");
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"http://localhost:5131/rest/v1/user/GetUserId?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return Ok(jsonData);
            }
            else
            {
                return BadRequest();
            }
        }

		[HttpPut]
		public async Task<IActionResult> UpdateUser(string id, [FromBody] ReqMstUserDto reqMstUserDto)
		{
			if (reqMstUserDto == null)
			{
				return BadRequest("Invalid user data.");
			}

			var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var json = JsonSerializer.Serialize(reqMstUserDto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync($"https://localhost:7218/rest/v1/user/UpdateUser?Id={id}", content);

			if (response.IsSuccessStatusCode)
			{
				var jsonData = await response.Content.ReadAsStringAsync();
				return Ok(jsonData);
			}
			else
			{
				return BadRequest("Failed to update user.");
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteUser(string id)
		{
			var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.DeleteAsync($"https://localhost:7218/rest/v1/user/Delete?id={id}");

			if (response.IsSuccessStatusCode)
			{
				return Ok("User deleted successfully.");
			}
			else
			{
				return BadRequest("Failed to delete user.");
			}
		}

		[HttpPost]
		public async Task<IActionResult> RegisterUser([FromBody] ReqRegisterDto reqRegisterUser)
		{
			if (reqRegisterUser == null)
			{
				return BadRequest("Invalid user data!");
			}

			var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var json = JsonSerializer.Serialize(reqRegisterUser);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("https://localhost:7218/rest/v1/user/Register", content);

			if (response.IsSuccessStatusCode)
			{
				var jsonData = await response.Content.ReadAsStringAsync();
				return Ok(jsonData);
			}
			else
			{
				return BadRequest(new { Message = "Failed to register user.", error = response.ReasonPhrase });
			}
		}
	}
}
