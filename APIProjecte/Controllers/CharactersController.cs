namespace WebAplicationAPIRestDemo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebAplicationAPIRestDemo.DAL.Service;

    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly CharacterService _service = new CharacterService();

        [HttpGet]
        public IActionResult GetAll()
        {
            var characters = _service.GetAll();
            return Ok(characters);
        }
    }

}
