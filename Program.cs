using Microsoft.Data.SqlClient;
using Tasks;

Console.WriteLine("Please, enter server name:");
string server = Console.ReadLine() ?? "localhost";
Console.WriteLine("Please, enter database name:");
string database = Console.ReadLine() ?? "tasks";

string connectionString = $"data source={server};initial catalog={database};trusted_connection=true;Encrypt=false;Connect Timeout = 5";
// DESKTOP-N96LJQD\SQLEXPRESS
// TasksDB

using (var connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Something went wrong... Check the message below:");
        Console.WriteLine(ex.Message);
    }

    if (connection.State == System.Data.ConnectionState.Open)
    {
        Console.WriteLine("You are connected to database.");

        TaskRepository db = new TaskRepository(connection);
        TodoApp app = new TodoApp(db);
        app.Run();
    }
}






