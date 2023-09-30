using Microsoft.AspNetCore.Mvc;
using Zerno.Data;
using Zerno.DTOs;
using Zerno.Models;

namespace Zerno.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IGrainStorage _db;
        const int PAGE_SIZE = 10;
        public UserController(IGrainStorage db)
        {
            _db = db;
        }
        [HttpGet]
        [Produces("application/hal+json")]
        public IActionResult Get(int index = 0, int count = PAGE_SIZE)
        {
            var items = _db.GetUsers(index, count).Select(v => v.ToResource());
            var total = _db.CountUsers();
            var _links = HAL.Paginate("/api/user", index, count, total);
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
            var user = _db.GetUserById(id);
            if (user == default)
                return NotFound();
            var resource = user.ToResource();
            resource._actions = new
            {
                delete = new
                {
                    href = $"api/user/{id}",
                    method = "DELETE",
                    name = $"delete user {id}"
                },
                update = new
                {
                    href = $"api/user/{id}",
                    method = "PUT",
                    name = $"update user {id}"
                }
            };
            return Ok(resource);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDTO dto)
        {
            var user = new User
            {
                Id = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName
            };
            _db.UpdateUser(user);
            return Ok(dto);
        }
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName
            };
            _db.CreateUser(user);
            return Ok(dto);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _db.GetUserById(id);
            _db.DeleteUser(product);
            return Ok(product.ToDynamic());
        }
    }
}
