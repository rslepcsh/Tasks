namespace Tasks.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public TodoTask(int id, string title, string description, bool isCompleted, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            IsCompleted = isCompleted;
            CreatedAt = createdAt;
        }
    }
}
