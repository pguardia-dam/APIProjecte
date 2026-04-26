using APIProjecte.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using WebAplicationAPIRestDemo.DAL.Service;

namespace WebAplicationAPIRestDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly SkillService _service = new SkillService();

        // Totes les habilitats disponibles per un personatge
        [HttpGet("character/{characterId}")]
        public IActionResult GetSkillsByCharacter(int characterId)
        {
            return Ok(_service.GetSkillsByCharacterId(characterId));
        }

        // Habilitats equipades per un usuari i personatge
        [HttpGet("equipped/{userId}/{characterId}")]
        public IActionResult GetEquippedSkills(int userId, int characterId)
        {
            return Ok(_service.GetEquippedSkills(userId, characterId));
        }

        // Equipar habilitat
        [HttpPost("equip")]
        public IActionResult EquipSkill([FromBody] EquipSkillDTO data)
        {
            bool ok = _service.EquipSkill(data.userId, data.characterId, data.skillId, data.slot);

            if (!ok)
                return BadRequest(new { message = "No s'ha pogut equipar l'habilitat" });

            return Ok(new { message = "Habilitat equipada correctament" });
        }
    
        // Des-equipar habilitat
        [HttpDelete("equip/{userId}/{characterId}/{slot}")]
        public IActionResult UnequipSkill(int userId, int characterId, int slot)
        {
            bool ok = _service.UnequipSkill(userId, characterId, slot);

            if (!ok)
                return BadRequest(new { message = "No s'ha pogut des-equipar l'habilitat" });

            return Ok(new { message = "Habilitat des-equipada" });
        }
    }
}
