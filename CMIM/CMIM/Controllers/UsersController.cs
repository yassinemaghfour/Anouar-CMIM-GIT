using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CMIM.Models;
using CMIM.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly CmimDBContext _context;
        public UsersController(IUserService userService, CmimDBContext context)
        {
            _userService = userService;
            _context = context;
            if (_context.Users.Where(U => U.Role == "Admin").Count() == 0)
            {
                _context.Users.Add(new CMIM.Models.User
                {
                    Etat = true,
                    FirstName = "Administrateur",
                    LastName = "Administrateur",
                    Password = "admin",
                    Role = Role.Admin,
                    Username = "admin"
                });
                _context.SaveChangesAsync();
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return NotFound(new { message = "Username or password is incorrect" });

            if (user.Etat == false)
                return BadRequest(new { message = "Le compte a été bloqué par l'administrateur... Veuillez le contacter" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _context.Users.Include("site").ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var User = _context.Users.Include(U => U.site).Where(U => U.Id == id).FirstOrDefault();

            if (User == null)
            {
                return NotFound();
            }

            

            return Ok(User);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] string id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var U = _context.Users.Find(user.Id);
            U.FirstName = user.FirstName;
            U.LastName = user.LastName;
            U.Email = user.Email;
            U.Etat = user.Etat;
            U.siteId = user.siteId;

            _context.Entry(U).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

    }
}