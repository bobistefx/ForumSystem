using AutomotiveForumSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveForumSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                // A connection string for establishing a connection to the locally installed SQL Server.
                // Set serverName to your local instance; databaseName is the name of the database
                string connectionString = @"Server=PLAMEN-LEGION\SQLEXPRESS;Database=AutomotiveForum;Trusted_Connection=True;";
                // Configure the application to use the locally installed SQL Server.
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            });

            var app = builder.Build();
            app.Run();
        }
    }
}