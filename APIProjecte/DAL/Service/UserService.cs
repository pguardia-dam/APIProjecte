using APIProjecte.DAL.Model;
using MySql.Data.MySqlClient;

namespace WebAplicationAPIRestDemo.DAL.Service
{
    public class UserService
    {
        public UserLogin Login(string mail, string password)
        {
            using (var conn = DbContext.GetInstance())
            {
                string query = "SELECT userID, mail, password FROM UserLogin WHERE mail = @mail AND password = @pass";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mail", mail);
                    cmd.Parameters.AddWithValue("@pass", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserLogin
                            {
                                userID = reader.GetInt32("userID"),
                                mail = reader.GetString("mail"),
                                password = reader.GetString("password")
                            };
                        }
                    }
                }
            }

            return null;
        }

        public bool UserExists(string mail)
        {
            using (var conn = DbContext.GetInstance())
            {
                string query = "SELECT COUNT(*) FROM UserLogin WHERE mail = @mail";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mail", mail);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool CreateUser(string mail, string password)
        {
            using (var conn = DbContext.GetInstance())
            {
                string query = "INSERT INTO UserLogin (mail, password) VALUES (@mail, @pass)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mail", mail);
                    cmd.Parameters.AddWithValue("@pass", password);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
