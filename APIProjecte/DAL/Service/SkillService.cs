using APIProjecte.DAL.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using WebAplicationAPIRestDemo.DAL.Model;

namespace WebAplicationAPIRestDemo.DAL.Service
{
    public class SkillService
    {
        public List<Skill> GetSkillsByCharacterId(int characterId)
        {
            var result = new List<Skill>();

            using (var conn = DbContext.GetInstance())
            {
                string query =
                    "SELECT idSkill, nameSkill, descriptionSkill, baseDamageSkill, " +
                    "energyCostSkill, dotSkill, isUnlockedSkill, skillTreePathSkill, characterIdSkill " +
                    "FROM Skill WHERE characterIdSkill = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", characterId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Skill
                            {
                                idSkill = reader.GetInt32("idSkill"),
                                nameSkill = reader.IsDBNull("nameSkill") ? "nullAPIBuilder" : reader.GetString("nameSkill"),
                                descriptionSkill = reader.IsDBNull("descriptionSkill") ? "nullAPIBuilder" : reader.GetString("descriptionSkill"),
                                baseDamageSkill = reader.IsDBNull("baseDamageSkill") ? 0 : reader.GetInt32("baseDamageSkill"),
                                energyCostSkill = reader.IsDBNull("energyCostSkill") ? 0 : reader.GetInt32("energyCostSkill"),
                                dotSkill = reader.IsDBNull("dotSkill") ? 0 : reader.GetInt32("dotSkill"),
                                isUnlockedSkill = reader.IsDBNull("isUnlockedSkill") ? false : reader.GetBoolean("isUnlockedSkill"),
                                skillTreePathSkill = reader.IsDBNull("skillTreePathSkill") ? 0 : reader.GetInt32("skillTreePathSkill"),
                                characterIdSkill = reader.GetInt32("characterIdSkill")

                            });
                        }
                    }
                }
            }

            return result;
        }


        public List<Skill> GetEquippedSkills(int characterId)
        {
            var result = new List<Skill>();

            using (var conn = DbContext.GetInstance())
            {
                string query =
                    "SELECT s.idSkill, s.nameSkill, s.descriptionSkill, s.baseDamageSkill, " +
                    "s.energyCostSkill, s.dotSkill, s.isUnlockedSkill, s.skillTreePathSkill, s.characterIdSkill " +
                    "FROM Skill s " +
                    "JOIN EquippedSkills eq ON s.idSkill = eq.SkillIdEQ " +
                    "WHERE eq.CharacterIdEQ = @id " +
                    "ORDER BY eq.skillPosition ASC";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", characterId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Skill
                            {
                                idSkill = reader.GetInt32("idSkill"),
                                nameSkill = reader.IsDBNull("nameSkill") ? "nullAPIBuilder" : reader.GetString("nameSkill"),
                                descriptionSkill = reader.IsDBNull("descriptionSkill") ? "nullAPIBuilder" : reader.GetString("descriptionSkill"),
                                baseDamageSkill = reader.IsDBNull("baseDamageSkill") ? 0 : reader.GetInt32("baseDamageSkill"),
                                energyCostSkill = reader.IsDBNull("energyCostSkill") ? 0 : reader.GetInt32("energyCostSkill"),
                                dotSkill = reader.IsDBNull("dotSkill") ? 0 : reader.GetInt32("dotSkill"),
                                isUnlockedSkill = reader.IsDBNull("isUnlockedSkill") ? false : reader.GetBoolean("isUnlockedSkill"),
                                skillTreePathSkill = reader.IsDBNull("skillTreePathSkill") ? 0 : reader.GetInt32("skillTreePathSkill"),
                                characterIdSkill = reader.GetInt32("characterIdSkill")

                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}
