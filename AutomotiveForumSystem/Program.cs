using AutomotiveForumSystem.Data;
using AutomotiveForumSystem.Helpers;
using AutomotiveForumSystem.Helpers.Contracts;
using AutomotiveForumSystem.Repositories;
using AutomotiveForumSystem.Repositories.Contracts;
using AutomotiveForumSystem.Services;
using AutomotiveForumSystem.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveForumSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();

            builder.Services.AddScoped<ICommentsService, CommentsService>();
            builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();

            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();

            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();

            builder.Services.AddScoped<ICategoryModelMapper, CategoryModelMapper>();
            builder.Services.AddScoped<ICommentModelMapper, CommentModelMapper>();
            builder.Services.AddScoped<IPostModelMapper, PostModelMapper>();
            builder.Services.AddScoped<IAuthManager, AuthManager>();
            builder.Services.AddScoped<IUserMapper, UserMapper>();

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                // A connection string for establishing a connection to the locally installed SQL Server.
                // Set serverName to your local instance; databaseName is the name of the database
                string connectionString = @"Server=localhost\SQLEXPRESS;Database=AutomotiveForum;Trusted_Connection=True;";

                // Configure the application to use the locally installed SQL Server.
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            });

            var app = builder.Build();
            app.MapControllers();
            app.Run();
        }
    }
}