using Microsoft.Data.SqlClient;
using Tasks.Repositories;
using Tasks.Services;

string connectionString = Connection.GetConnectionString();

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






