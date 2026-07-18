using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Models;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_userService.GetAll());
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} was not found.");
            }
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = _userService.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = created.Id }, created);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = _userService.Update(id, user);
            if (!updated)
            {
                return NotFound($"User with ID {id} was not found.");
            }

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var deleted = _userService.Delete(id);
            if (!deleted)
            {
                return NotFound($"User with ID {id} was not found.");
            }

            return NoContent();
        }
    }
}
