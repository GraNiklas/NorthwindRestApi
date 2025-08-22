using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NorthwindOriginalContext _context;
        public UsersController(NorthwindOriginalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                user.Password = null; // Remove password for security reasons
            }
            return Ok(users);
        }
        [HttpPost]
        public ActionResult PostCreateNew([FromBody] User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok("User created successfully: " + user.Username);
            }
            catch (Exception e)
            {

                return BadRequest("Error creating user: " + e.Message );
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound($"User id:llä {id} ei löytynyt.");
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok("User poistettu: " + user.Username);
            }
            catch (Exception e)
            {
                return BadRequest("Errormessage: " + e.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult EditUser(int id, [FromBody] User editedUser)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound($"User id:llä {id} ei löytynyt.");
                }
                user.AccesslevelId = editedUser.AccesslevelId;
                user.FirstName = editedUser.FirstName;
                user.LastName = editedUser.LastName;
                user.Username = editedUser.Username;
                user.Email = editedUser.Email;
                user.Password = editedUser.Password; // Ensure password is updated

                _context.SaveChanges();
                return Ok("User muokattu id: " + user.Username);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
 