using Microsoft.AspNetCore.Mvc;
using WebAplicationAPIRestDemo.DAL.Service;

namespace WebAplicationAPIRestDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly SkillService _service = new SkillService();

        // Totes les habilitats del personatge
        [HttpGet("character/{id}")]
        public IActionResult GetSkillsByCharacter(int id)
        {
            var skills = _service.GetSkillsByCharacterId(id);
            return Ok(skills);
        }

        // Només les habilitats equipades (les 4 del combat)
        [HttpGet("equipped/{id}")]
        public IActionResult GetEquippedSkills(int id)
        {
            var skills = _service.GetEquippedSkills(id);
            return Ok(skills);
        }
    }
}
