using Microsoft.AspNetCore.Mvc;
namespace Auto.API.Controllers;

[Route("api")]
[ApiController]
public class DiscoveryEndpointController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var welcome = new
        {
            _links = new
            {
                products = new
                {
                    href = "/api/product"
                },
                users = new
                {
                    href = "/api/user"
                }
            },
            message = "Welcome to the Grain API!",
        };
        return Ok(welcome);
    }
}