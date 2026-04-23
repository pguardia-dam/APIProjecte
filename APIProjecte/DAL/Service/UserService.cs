using APIProjecte.DAL.Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebAplicationAPIRestDemo.DAL.Service
{
    public class UserService
    {
        public UserService()
        {
            
        }

        public User GetUser(string mail, string password)
        {
            User result = null;

            using (var conn = DbContext.GetInstance())
            {
                string query =
                    "SELECT userID, mail, password " +
                    "FROM User WHERE mail = @mail AND password = @password";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mail", mail);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new User
                            {
                                userID = reader.GetInt32("userID"),
                                mail = reader.IsDBNull("mail") ? "nullAPIBuilder" : reader.GetString("mail"),
                                password = reader.IsDBNull("password") ? "nullAPIBuilder" : reader.GetString("password")
                            };
                        }
                    }
                }
            }

            return result;
        }

        public bool UserExists(string mail)
        {
            using (var conn = DbContext.GetInstance())
            {
                string query = "SELECT COUNT(*) FROM User WHERE mail = @mail";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mail", mail);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public bool CreateUser(string mail, string password)
        {
            using (var conn = DbContext.GetInstance())
            {
                string query =
                    "INSERT INTO User (mail, password) VALUES (@mail, @password)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mail", mail);
                    cmd.Parameters.AddWithValue("@password", password);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}