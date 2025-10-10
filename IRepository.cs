namespace Tasks
{
    public interface IRepository<TodoTask>
    {
        IEnumerable<TodoTask> GetAllTasks();
        TodoTask GetTaskById(int id);
        void AddNewTask(TodoTask task);
        void UpdateTaskById(int id);
        void DeleteTaskById(int id);
    }
}
