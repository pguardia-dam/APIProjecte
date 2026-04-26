using APIProjecte.DAL.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace WebAplicationAPIRestDemo.DAL.Service
{
    public class SkillService
    {
        // ---------------------------------------------------------
        // TOTES LES HABILITATS DISPONIBLES PER UN PERSONATGE
        // ---------------------------------------------------------
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

        // ---------------------------------------------------------
        // HABILITATS EQUIPADES PER UN USUARI I PERSONATGE
        // ---------------------------------------------------------
        public List<Skill> GetEquippedSkills(int userId, int characterId)
        {
            var result = new List<Skill>();

            using (var conn = DbContext.GetInstance())
            {
                string query =
                    "SELECT s.idSkill, s.nameSkill, s.descriptionSkill, s.baseDamageSkill, " +
                    "s.energyCostSkill, s.dotSkill, s.isUnlockedSkill, s.skillTreePathSkill, s.characterIdSkill " +
                    "FROM Skill s " +
                    "JOIN EquippedSkills eq ON s.idSkill = eq.SkillIdEQ " +
                    "WHERE eq.UserId = @user AND eq.CharacterIdEQ = @char " +
                    "ORDER BY eq.skillPosition ASC";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", userId);
                    cmd.Parameters.AddWithValue("@char", characterId);

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

        // ---------------------------------------------------------
        // EQUIPAR UNA HABILITAT
        // ---------------------------------------------------------
        public bool EquipSkill(int userId, int characterId, int skillId, int slot)
        {
            using (var conn = DbContext.GetInstance())
            {
                // 1. Esborrem la que hi ha al slot
                string deleteQuery =
                    "DELETE FROM EquippedSkills WHERE UserId = @user AND CharacterIdEQ = @char AND skillPosition = @slot";

                using (var deleteCmd = new MySqlCommand(deleteQuery, conn))
                {
                    deleteCmd.Parameters.AddWithValue("@user", userId);
                    deleteCmd.Parameters.AddWithValue("@char", characterId);
                    deleteCmd.Parameters.AddWithValue("@slot", slot);
                    deleteCmd.ExecuteNonQuery();
                }

                // 2. Inserim la nova
                string insertQuery =
                    "INSERT INTO EquippedSkills (UserId, CharacterIdEQ, SkillIdEQ, skillPosition) " +
                    "VALUES (@user, @char, @skill, @slot)";

                using (var insertCmd = new MySqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@user", userId);
                    insertCmd.Parameters.AddWithValue("@char", characterId);
                    insertCmd.Parameters.AddWithValue("@skill", skillId);
                    insertCmd.Parameters.AddWithValue("@slot", slot);

                    return insertCmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // ---------------------------------------------------------
        // DES-EQUIPAR UNA HABILITAT
        // ---------------------------------------------------------
        public bool UnequipSkill(int userId, int characterId, int slot)
        {
            using (var conn = DbContext.GetInstance())
            {
                string query =
                    "DELETE FROM EquippedSkills WHERE UserId = @user AND CharacterIdEQ = @char AND skillPosition = @slot";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", userId);
                    cmd.Parameters.AddWithValue("@char", characterId);
                    cmd.Parameters.AddWithValue("@slot", slot);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
