using Microsoft.AspNetCore.Mvc;
using Zerno.DTOs;
using Zerno.Models;
using Zerno.Services;

namespace Zerno.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IGrainStorage _db;
        const int PAGE_SIZE = 10;
        public ProductController(IGrainStorage db)
        {
            _db = db;
        }
        [HttpGet]
        [Produces("application/hal+json")]
        public IActionResult Get(int index = 0, int count = PAGE_SIZE)
        {
            var items = _db.GetProducts(index, count).Select(v => v.ToResource());
            var total = _db.CountProducts();
            var _links = HAL.Paginate("/api/product", index, count, total);
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
            var product = _db.GetProductById(id);
            if(product == default)
                return NotFound();
            var resource = product.ToResource();
            resource._actions = new
            {
                delete = new
                {
                    href = $"api/product/{id}",
                    method = "DELETE",
                    name = $"delete product {id}"
                },
                update = new
                {
                    href = $"api/product/{id}",
                    method = "PUT",
                    name = $"update product {id}"
                }
            };
            return Ok(resource);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductDTO dto)
        {
            var product = new Product
            {
                Id = id,
                DealerId = dto.DealerId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Price = dto.Price,
                FullAmmount = dto.FullAmmount,
                Ammount = dto.Ammount,
                Type = dto.Type
            };
            _db.UpdateProduct(product);
            return Ok(dto);
        }
        [HttpPost]
        public IActionResult Post([FromBody] ProductDTO dto)
        {
            var product = new Product
            {
                DealerId = dto.DealerId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Price = dto.Price,
                FullAmmount = dto.FullAmmount,
                Ammount = dto.Ammount,
                Type = dto.Type
            };
            _db.CreateProduct(product);
            return Ok(dto);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _db.GetProductById(id);
            _db.DeleteProduct(product);
            return Ok(product.ToDynamic());
        }
    }
}
