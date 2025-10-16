namespace Tasks.Interfaces
{
    public interface IRepository<T>
    {
        Task<IReadOnlyCollection<T>> GetAllTasksAsync();
        Task<T> GetTaskByIdAsync(int id);
        Task AddNewTaskAsync(T task);
        Task UpdateTaskByIdAsync(int id);
        Task DeleteTaskByIdAsync(int id);
    }
}
