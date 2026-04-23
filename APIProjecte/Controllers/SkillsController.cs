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

        [HttpGet("character/{id}")]
        public IActionResult GetSkillsByCharacter(int id)
        {
            return Ok(_service.GetSkillsByCharacterId(id));
        }

        [HttpGet("equipped/{id}")]
        public IActionResult GetEquippedSkills(int id)
        {
            return Ok(_service.GetEquippedSkills(id));
        }

        // -----------------------------
        // EQUIPAR UNA HABILITAT
        // -----------------------------
        [HttpPost("equip")]
        public IActionResult EquipSkill([FromBody] EquipSkillDTO data)
        {
            bool ok = _service.EquipSkill(data.characterId, data.skillId, data.slot);

            if (!ok)
                return BadRequest(new { message = "No s'ha pogut equipar l'habilitat" });

            return Ok(new { message = "Habilitat equipada correctament" });
        }

        // -----------------------------
        // DES-EQUIPAR UNA HABILITAT
        // -----------------------------
        [HttpDelete("equip/{characterId}/{slot}")]
        public IActionResult UnequipSkill(int characterId, int slot)
        {
            bool ok = _service.UnequipSkill(characterId, slot);

            if (!ok)
                return BadRequest(new { message = "No s'ha pogut des-equipar l'habilitat" });

            return Ok(new { message = "Habilitat des-equipada" });
        }
    }
}
