namespace WebAplicationAPIRestDemo.DAL.Service
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using WebAplicationAPIRestDemo.DAL.Model;
    //using WebApplicationAPIDemo.Persistence;

    public class CharacterService
    {
        public List<Character> GetAll()
        {
            var result = new List<Character>();

            using (var conn = DbContext.GetInstance())
            {
                string query = "SELECT idCharacter, " +
                               "nameCharacter, " +
                               "healthCharacter, " +
                               "levelCharacter, " +
                               "skillTreePath, " +
                               "teamPosition, " +
                               "enemy " +
                               "FROM playeableCharacter";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Character
                        {
                            idCharacter = reader["idCharacter"] != DBNull.Value ? reader.GetInt32("idCharacter") : 0,
                            nameCharacter = reader["nameCharacter"] != DBNull.Value ? reader.GetString("nameCharacter") : "",
                            healthCharacter = reader["healthCharacter"] != DBNull.Value ? reader.GetInt32("healthCharacter") : 0,
                            levelCharacter = reader["levelCharacter"] != DBNull.Value ? reader.GetInt32("levelCharacter") : 1,
                            skillTreePath = reader["skillTreePath"] != DBNull.Value ? reader.GetInt32("skillTreePath") : 0,
                            teamPosition = reader["teamPosition"] != DBNull.Value ? reader.GetInt32("teamPosition") : 1,
                            isEnemy = reader["enemy"] != DBNull.Value ? reader.GetBoolean("enemy") : false
                        });

                    }
                }
            }

            return result;
        }
    }

}
