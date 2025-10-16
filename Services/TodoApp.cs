using Tasks.Interfaces;
using Tasks.Models;

namespace Tasks.Services
{
    public class TodoApp
    {
        //public IRepository<TodoTask> _repository { get; set; }
        private readonly IRepository<TodoTask> _repository;
        public TodoApp(IRepository<TodoTask> db)
        {
            _repository = db;
        }
        public void Run()
        {

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("----------------------");
                Console.WriteLine("Enter a number below:");
                Console.WriteLine("----------------------");
                Console.WriteLine("1 - Show all tasks");
                Console.WriteLine("2 - Show task by Id");
                Console.WriteLine("3 - Create new task");
                Console.WriteLine("4 - Update task by Id");
                Console.WriteLine("5 - Delete task by Id");
                Console.WriteLine("6 - Exit program");

                string answer = Console.ReadLine() ?? string.Empty;
                Console.Clear();

                switch (answer)
                {
                    case "1":
                        GetAllTasks();
                        break;
                    case "2":
                        GetTaskById();
                        break;
                    case "3":
                        AddNewTask();
                        break;
                    case "4":
                        UpdateStatusById();
                        break;
                    case "5":
                        DeleteTaskById();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Please, enter a valid option.");
                        break;
                }
            }
        }

        public async void GetAllTasks()
        {
            var tasks = await _repository.GetAllTasksAsync();

            Console.WriteLine("");
            Console.WriteLine("All available tasks:");
            Console.WriteLine("---------------------");
            foreach (var task in tasks)
            {
                Console.WriteLine($"Title: {task.Title}, Id: {task.Id}");
            }
        }

        public async void GetTaskById()
        {
            int id = await GetId();

            if (id == 0)
            {
                Console.WriteLine("There is no such ID, try again");
            }
            else
            {
                var taskById = await _repository.GetTaskByIdAsync(id);

                Console.WriteLine("");
                Console.WriteLine($"{taskById.Title} - {taskById.CreatedAt.ToString()}");
                Console.WriteLine("------------------");
                Console.WriteLine($"{taskById.Description}");
                Console.WriteLine($"Completed: {taskById.IsCompleted}");
            }
        }

        public async void GetTaskById(int id)
        {
            var taskById = await _repository.GetTaskByIdAsync(id);

            Console.WriteLine("");
            Console.WriteLine($"{taskById.Title} - {taskById.CreatedAt.ToString()}");
            Console.WriteLine("------------------");
            Console.WriteLine($"{taskById.Description}");
            Console.WriteLine($"Completed: {taskById.IsCompleted}");
        }

        public async void AddNewTask()
        {
            Console.WriteLine("Please, enter the title:");
            string title = Console.ReadLine() ?? "Title";

            Console.WriteLine("Enter description:");
            string description = Console.ReadLine() ?? "Description";

            TodoTask newTask = new TodoTask(0, title, description, false, DateTime.Now);

            await _repository.AddNewTaskAsync(newTask);

            GetAllTasks();
        }

        public async void UpdateStatusById()
        {
            int id = await GetId();

            if (id == 0)
            {
                Console.WriteLine("There is no such ID, try again");
            }
            else
            {
                await _repository.UpdateTaskByIdAsync(id);

                Console.WriteLine("");
                Console.WriteLine("Updated task:");
                GetTaskById(id);
            }
        }

        public async void DeleteTaskById()
        {
            int id = await GetId();

            if (id == 0)
            {
                Console.WriteLine("There is no such ID, Try again");
            }
            else
            {
                await _repository.DeleteTaskByIdAsync(id);

                Console.WriteLine("");
                Console.WriteLine("Deleted!");
                GetAllTasks();
            }
        }

        public async Task<int> GetId()
        {
            Console.WriteLine("What Id number do you want to use?");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                var tasks = await _repository.GetAllTasksAsync();
                foreach (TodoTask task in tasks)
                {
                    if (id == task.Id)
                    {
                        return id;
                    }
                }
            }
            return 0;
        }
    }
}
