using Microsoft.AspNetCore.Mvc;

namespace Zerno.Controllers
{
    [Route("[controller]")]
    public class TestSignalRController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
