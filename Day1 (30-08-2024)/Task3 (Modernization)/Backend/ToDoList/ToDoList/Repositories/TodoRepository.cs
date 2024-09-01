using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TodoApp.Models;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Utils;

namespace ToDoList.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private const string InsertTodosSql = "INSERT INTO todos (title, username, description, target_date, is_done) VALUES (@Title, @Username, @Description, @TargetDate, @IsDone);";
        private const string SelectTodoByIdSql = "SELECT id, title, username, description, target_date, is_done FROM todos WHERE id = @Id";
        private const string SelectAllTodosSql = "SELECT * FROM todos";
        private const string DeleteTodoByIdSql = "DELETE FROM todos WHERE id = @Id;";
        private const string UpdateTodoSql = "UPDATE todos SET title = @Title, username = @Username, description = @Description, target_date = @TargetDate, is_done = @IsDone WHERE id = @Id;";

        public async Task InsertTodoAsync(Todo todo)
        {
            try
            {
                using (SqlConnection connection = DatabaseUtils.GetConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand(InsertTodosSql, connection))
                        {
                            command.Parameters.AddWithValue("@Title", todo.Title);
                            command.Parameters.AddWithValue("@Username", todo.Username);
                            command.Parameters.AddWithValue("@Description", todo.Description);
                            command.Parameters.AddWithValue("@TargetDate", todo.TargetDate);
                            command.Parameters.AddWithValue("@IsDone", todo.Status);

                            await command.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                DatabaseUtils.PrintSqlException(ex);
            }
        }

        public async Task<Todo> SelectTodoAsync(long todoId)
        {
            Todo todo = null;

            try
            {
                using (SqlConnection connection = DatabaseUtils.GetConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand(SelectTodoByIdSql, connection))
                        {
                            command.Parameters.AddWithValue("@Id", todoId);

                            using (SqlDataReader reader = await command.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    todo = new Todo
                                    {
                                        Id = reader.GetInt64("id"),
                                        Title = reader.GetString("title"),
                                        Username = reader.GetString("username"),
                                        Description = reader.GetString("description"),
                                        TargetDate = reader.GetDateTime("target_date"),
                                        Status = reader.GetBoolean("is_done")
                                    };
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                DatabaseUtils.PrintSqlException(ex);
            }

            return todo;
        }

        public async Task<List<Todo>> SelectAllTodosAsync()
        {
            var todos = new List<Todo>();

            try
            {
                using (SqlConnection connection = DatabaseUtils.GetConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand(SelectAllTodosSql, connection))
                        {
                            using (SqlDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var todo = new Todo
                                    {
                                        Id = reader.GetInt64("id"),
                                        Title = reader.GetString("title"),
                                        Username = reader.GetString("username"),
                                        Description = reader.GetString("description"),
                                        TargetDate = reader.GetDateTime("target_date"),
                                        Status = reader.GetBoolean("is_done")
                                    };
                                    todos.Add(todo);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                DatabaseUtils.PrintSqlException(ex);
            }

            return todos;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            bool rowDeleted = false;

            try
            {
                using (SqlConnection connection = DatabaseUtils.GetConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand(DeleteTodoByIdSql, connection))
                        {
                            command.Parameters.AddWithValue("@Id", id);

                            rowDeleted = await command.ExecuteNonQueryAsync() > 0;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                DatabaseUtils.PrintSqlException(ex);
            }

            return rowDeleted;
        }

        public async Task<bool> UpdateTodoAsync(Todo todo)
        {
            bool rowUpdated = false;

            try
            {
                using (SqlConnection connection = DatabaseUtils.GetConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand(UpdateTodoSql, connection))
                        {
                            command.Parameters.AddWithValue("@Title", todo.Title);
                            command.Parameters.AddWithValue("@Username", todo.Username);
                            command.Parameters.AddWithValue("@Description", todo.Description);
                            command.Parameters.AddWithValue("@TargetDate", todo.TargetDate);
                            command.Parameters.AddWithValue("@IsDone", todo.Status);
                            command.Parameters.AddWithValue("@Id", todo.Id);

                            rowUpdated = await command.ExecuteNonQueryAsync() > 0;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                DatabaseUtils.PrintSqlException(ex);
            }

            return rowUpdated;
        }
    }
}
