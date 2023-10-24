using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Zerno.DTOs;
using Zerno.Models;
using Zerno.Services;

namespace Zerno.Controllers
{
    [Route("api/[controller]")]
    public class RequestController : Controller
    {
        private readonly IBus _bus;
        private readonly IGrainStorage _db;
        public RequestController(IGrainStorage db, IBus bus)
        {
            _db = db;
            _bus = bus;
        }

        [HttpGet("byProduct/{id}")]
        [Produces("application/hal+json")]
        public IActionResult GetByProduct(int id, int index = 0, int count = int.MaxValue)
        {
            var tmpItems = _db.GetRequestsByProductId(id);
            var total = tmpItems.Count;
            var items = tmpItems.Skip(index).Take(count).Select(v => v.ToResource());
            var _links = HAL.Paginate("/api/request/byProduct", index, count, total);
            var result = new
            {
                _links,
                count,
                total,
                index,
                items
            };
            return Ok(result);
        }

        [HttpGet("byUser/{id}")]
        [Produces("application/hal+json")]
        public IActionResult GetByUser(int id, int index = 0, int count = int.MaxValue)
        {
            var tmpItems = _db.GetRequestsByUserId(id);
            var total = tmpItems.Count;
            var items = tmpItems.Skip(index).Take(count).Select(v => v.ToResource());
            var _links = HAL.Paginate("/api/request/byUser", index, count, total);
            var result = new
            {
                _links,
                count,
                total,
                index,
                items
            };
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Produces("application/hal+json")]
        public IActionResult Get(int id)
        {
            var request = _db.GetRequestById(id);
            if (request == default)
                return NotFound();
            var resource = request.ToResource();
            resource._actions = new
            {
                delete = new
                {
                    href = $"api/request/{id}",
                    method = "DELETE",
                    name = $"delete request {id}"
                }
            };
            return Ok(resource);
        }
       
        [HttpPost]
        public IActionResult Post([FromBody] RequestDTO dto)
        {
            var request = new Request
            {
                Ammount = dto.Ammount,
                ProductId = dto.ProductId,
                Date = dto.Date,
                WanterId = dto.WanterId
            };
            _db.CreateRequest(request);
            request.Product = _db.GetProductById(dto.ProductId);
            request.Wanter = _db.GetUserById(dto.WanterId);
            PublishRequest(request);
            return Ok(dto);
        }

        private async Task PublishRequest(Request request) => await _bus.PubSub.PublishAsync(request.ToMessage());

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var request = _db.GetRequestById(id);
            _db.DeleteRequest(request);
            return Ok(request.ToDynamic());
        }
    }
}
