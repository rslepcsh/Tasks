using Dapper;
using Microsoft.Data.SqlClient;
using Tasks.Interfaces;
using Tasks.Models;

namespace Tasks.Repositories
{
    internal class TaskRepository : IRepository<TodoTask>
    {
        public SqlConnection Connection { get; init; }
        public TaskRepository(SqlConnection connection)
        {
            Connection = connection;
        }

        public async Task<IReadOnlyCollection<TodoTask>> GetAllTasksAsync()
        {
            var tasks = await Connection.QueryAsync<TodoTask>("SELECT * FROM Tasks");
            return tasks.ToList().AsReadOnly();
        }

        public async Task<TodoTask> GetTaskByIdAsync(int id)
        {
            return await Connection.QuerySingleAsync<TodoTask>("SELECT * FROM Tasks WHERE Id = @id", new { id });
        }

        public async Task AddNewTaskAsync(TodoTask task)
        {
            string query = @"
                INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt) 
                VALUES (@Title, @Description, @IsCompleted, @CreatedAt)
            ";

            await Connection.ExecuteAsync(query, task);
        }

        public async Task UpdateTaskByIdAsync(int id)
        {
            string query = @"
                UPDATE Tasks
                SET IsCompleted = CASE
                    WHEN IsCompleted < 1 THEN 1
                    ELSE 0
                END
                WHERE Id = @Id
            ";

            await Connection.ExecuteAsync(query, new { id });
        }

        public async Task DeleteTaskByIdAsync(int id)
        {
            await Connection.ExecuteAsync("DELETE FROM Tasks WHERE Id = @id", new { id });
        }
    }
}
