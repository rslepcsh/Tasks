namespace Tasks
{
    public class TodoApp
    {
        public IRepository<TodoTask> DB { get; set; }
        public TodoApp(IRepository<TodoTask> db) {
            DB = db;
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

                switch (answer)
                {
                    case "1":
                        Console.Clear();
                        GetAllTasks();
                        break;
                    case "2":
                        Console.Clear();
                        GetTaskById();
                        break;
                    case "3":
                        Console.Clear();
                        AddNewTask();
                        break;
                    case "4":
                        Console.Clear();
                        UpdateStatusById();
                        break;
                    case "5":
                        Console.Clear();
                        DeleteTaskById();
                        break;
                    case "6":
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Please, enter a valid option.");
                        break;
                }
            }
        }

        public void GetAllTasks()
        {
            IEnumerable<TodoTask> tasks = DB.GetAllTasks();

            Console.WriteLine("All available tasks:");
            Console.WriteLine("---------------------");
            Console.WriteLine("");
            foreach (TodoTask task in tasks)
            {
                Console.WriteLine($"Title: {task.Title}, Id: {task.Id}");
            }
        }

        public void GetTaskById()
        {
            int id = GetId();

            if (id == 0)
            {
                Console.WriteLine("There is no such ID, try again");
            }
            else
            {
                TodoTask taskById = DB.GetTaskById(id);

                Console.WriteLine($"{taskById.Title} - {taskById.CreatedAt.ToString()}");
                Console.WriteLine("------------------");
                Console.WriteLine($"{taskById.Description}");
                Console.WriteLine($"Completed: {taskById.IsCompleted}");
            }
        }

        public void GetTaskById(int id)
        {
            TodoTask taskById = DB.GetTaskById(id);

            Console.WriteLine($"{taskById.Title} - {taskById.CreatedAt.ToString()}");
            Console.WriteLine("------------------");
            Console.WriteLine($"{taskById.Description}");
            Console.WriteLine($"Completed: {taskById.IsCompleted}");
        }

        public void AddNewTask()
        {
            Console.WriteLine("Please, enter the title:");
            string title = Console.ReadLine() ?? "Title";

            Console.WriteLine("Enter description:");
            string description = Console.ReadLine() ?? "Description";

            TodoTask newTask = new TodoTask(0, title, description, false, DateTime.Now);

            DB.AddNewTask(newTask);

            GetAllTasks();
        }

        public void UpdateStatusById()
        {
            int id = GetId();

            if (id ==0)
            {
                Console.WriteLine("There is no such ID, try again");
            }
            else
            {
                Console.WriteLine("Change status ('no to decline)?");
                string answer = Console.ReadLine() ?? string.Empty;

                if (answer.ToLower() != "no")
                {
                    DB.UpdateTaskById(id);
                }

                Console.WriteLine("Updated task:");
                GetTaskById(id);
            }
        }

        public void DeleteTaskById()
        {
            int id = GetId();

            if (id == 0)
            {
                Console.WriteLine("There is no such ID, Try again");
            }
            else
            {
                Console.WriteLine("Are you sure ('no' to decline)?");
                string answer = Console.ReadLine() ?? string.Empty;

                if (answer.ToLower() != "no")
                {
                    DB.DeleteTaskById(id);
                    Console.WriteLine("Deleted!");
                }

                GetAllTasks();
            }
        }

        public int GetId()
        {
            Console.WriteLine("What Id number do you want to use?");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                IEnumerable<TodoTask> tasks = DB.GetAllTasks();
                foreach (TodoTask task in tasks) {
                    if (id == task.Id) {
                        return id;
                    }
                }
            }
            return 0;
        }
    }
}
