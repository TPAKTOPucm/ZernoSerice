using Microsoft.AspNetCore.Mvc;

namespace Zerno.Controllers
{
    [Route("requests")]
    public class TestSignalRController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
