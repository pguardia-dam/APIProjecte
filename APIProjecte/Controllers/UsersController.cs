using APIProjecte.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using WebAplicationAPIRestDemo.DAL.Service;

namespace APIProjecte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController()
        {
            _userService = new UserService();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginData)
        {
            var user = _userService.GetUser(loginData.mail, loginData.password);

            if (user == null)
                return Unauthorized(new { message = "Credencials incorrectes" });

            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            if (_userService.UserExists(newUser.mail))
                return BadRequest(new { message = "El correu ja esta registrat" });

            bool created = _userService.CreateUser(newUser.mail, newUser.password);

            if (!created)
                return BadRequest(new { message = "No s'ha pogut crear l'usuari" });

            return Ok(new { message = "Usuari creat correctament" });
        }

        [HttpGet("exists/{mail}")]
        public IActionResult Exists(string mail)
        {
            bool exists = _userService.UserExists(mail);
            return Ok(new { exists });
        }
    }
}