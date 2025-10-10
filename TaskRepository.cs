using Dapper;
using Microsoft.Data.SqlClient;

namespace Tasks
{
    internal class TaskRepository: IRepository<TodoTask>
    {
        public SqlConnection Connection { get; init; }
        public TaskRepository (SqlConnection connection)
        {
            Connection = connection;
        }

        public IEnumerable<TodoTask> GetAllTasks()
        {
            IEnumerable<TodoTask> tasks = Connection.Query<TodoTask>("Select * From Tasks");
            return tasks;
        }

        public TodoTask GetTaskById(int id) {
            TodoTask taskById = Connection.QuerySingle<TodoTask>("Select * From Tasks Where Id = @id", new { id });
            return taskById;
        }

        public void AddNewTask(TodoTask task)
        {
            Connection.Execute($"Insert Into Tasks (Title, Description, IsCompleted, CreatedAt) Values (@Title, @Description, @IsCompleted, @CreatedAt)", task);
        }

        public void UpdateTaskById(int id)
        {
            Connection.Execute("Update Tasks Set IsCompleted = Case When IsCompleted < 1 Then 1 Else 0 End Where Id = @id", new { id });
        }

        public void DeleteTaskById(int id) {
            Connection.Execute("Delete From Tasks Where Id = @id", new { id });
        }
    }
}
