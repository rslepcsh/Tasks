using System;
using System.Collections.Generic;
using System.Linq;
namespace Tasks.Services
{
    internal class Connection
    {
        public static string GetConnectionString()
        {
            Console.WriteLine("Please, enter server name:");
            string server = Console.ReadLine() ?? "localhost";
            Console.WriteLine("Please, enter database name:");
            string database = Console.ReadLine() ?? "tasks";

            return $"data source={server};initial catalog={database};trusted_connection=true;Encrypt=false;Connect Timeout = 5";
        }
    }
}
