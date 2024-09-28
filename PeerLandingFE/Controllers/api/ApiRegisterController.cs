using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using PeerLandingFE.DTO.Req;
using PeerLandingFE.DTO.Res;
using PeerLandingFE.Models;

namespace PeerLandingFE.Controllers.api
{
    public class ApiRegisterController : Controller
    {
        [HttpPost]
        public IActionResult Register([FromBody] ReqRegisterDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request.");
            }

            // Logika untuk menyimpan pengguna baru
            // Misalnya, simpan ke database
            // var newUser = new User { ... };
            // _context.Users.Add(newUser);
            // await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful" });
        }
    }
}

