using System.Data.SqlClient;
using System.Threading.Tasks;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Utils;

namespace ToDoList.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<int> RegisterUserAsync(User user)
        {
            int result = 0;
            var query = "INSERT INTO users (first_name, last_name, username, password) VALUES (@FirstName, @LastName, @Username, @Password);";

            try
            {
                using (SqlConnection connection = DatabaseUtils.GetConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", user.FirstName);
                            command.Parameters.AddWithValue("@LastName", user.LastName);
                            command.Parameters.AddWithValue("@Username", user.Username);
                            command.Parameters.AddWithValue("@Password", user.Password);

                            result = await command.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                DatabaseUtils.PrintSqlException(ex);
            }

            return result;
        }
    }
}
