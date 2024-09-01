using System.Data.SqlClient;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Utils;
using System.Threading.Tasks;

namespace ToDoList.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        public async Task<bool> ValidateAsync(LoginDto loginDto)
        {
            bool status = false;

            var query = "SELECT * FROM users WHERE username = @Username AND password = @Password";
            try
            {
                using (SqlConnection connection = DatabaseUtils.GetConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Username", loginDto.Username);
                            command.Parameters.AddWithValue("@Password", loginDto.Password);

                            using (SqlDataReader reader = await command.ExecuteReaderAsync())
                            {
                                status = await reader.ReadAsync();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                DatabaseUtils.PrintSqlException(ex);
            }

            return status;
        }
    }
}
