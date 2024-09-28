using Microsoft.AspNetCore.Mvc;

namespace PeerLandingFE.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View("Register/Index");
        }
    }
}
