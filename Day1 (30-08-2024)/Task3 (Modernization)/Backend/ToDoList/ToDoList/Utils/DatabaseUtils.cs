using System;
using System.Data.SqlClient;

namespace ToDoList.Utils
{
    public static class DatabaseUtils
    {
        private static readonly string connectionString = "Data Source=G73CBX3\\SQLEXPRESS;Integrated Security=true;Initial Catalog=TodoListApp";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (SqlException ex)
            {
                PrintSqlException(ex);
            }
            return connection;
        }

        public static void PrintSqlException(SqlException ex)
        {
            Console.Error.WriteLine($"SQLState: {ex.Number}");
            Console.Error.WriteLine($"Error Code: {ex.Number}");
            Console.Error.WriteLine($"Message: {ex.Message}");
            Exception innerException = ex.InnerException;
            while (innerException != null)
            {
                Console.Error.WriteLine($"Cause: {innerException}");
                innerException = innerException.InnerException;
            }
        }

        public static DateTime GetDateTime(DateTime date)
        {
            return date;
        }

        public static DateTime GetDateOnly(DateTime date)
        {
            return date.Date;
        }
    }
}
