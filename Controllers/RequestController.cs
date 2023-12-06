using EasyNetQ;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Zerno.DTOs;
using Zerno.Models;
using Zerno.PriceService;
using Zerno.Services;

namespace Zerno.Controllers
{
    [Route("api/[controller]")]
    public class RequestController : Controller
    {
        private readonly IBus _bus;
        private readonly IGrainStorage _db;
        private GrpcChannel channel;
        private Pricer.PricerClient grpcClient;
        const string SIGNALR_HUB_URL = "http://localhost:5231/hub";
        private static HubConnection hub;
        public RequestController(IGrainStorage db, IBus bus)
        {
            _db = db;
            _bus = bus;

            channel = GrpcChannel.ForAddress("http://localhost:5000");
            grpcClient = new Pricer.PricerClient(channel);

            hub = new HubConnectionBuilder().WithUrl(SIGNALR_HUB_URL).Build();
            hub.StartAsync();
        }

        ~RequestController() {
            channel.Dispose();
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
            try
            {
                PublishRequest(request);
                hub.SendAsync("NotifyWebUsers", $"{request.Wanter.FirstName} {request.Wanter.LastName}", $"создал запрос на покупку {request.Product.Type} в количестве {request.Ammount} кг. Рекоменндуемая стоимость: {GetPrice(request.Product)}");
            }
            catch (Exception) { }
            return Ok(dto);
        }

        private int GetPrice(Product product)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var grpcClient = new Pricer.PricerClient(channel);
            var request = new PriceRequest
            {
                Type = product.Type.ToString(),
                Sort = product.Type == SeedType.Зерно ? 1 : 2,
                Region = "Moscow"
            };
            var reply = grpcClient.GetPrice(request);
            return reply.Price;
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
