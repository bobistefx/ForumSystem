using AutomotiveForumSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveForumSystem.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed users

            List<User> users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    UserName = "admin",
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john@mail.com",
                    Password = "1234",
                    PhoneNumber = "0888 102 030",
                    IsAdmin = true,
                },
                new User()
                {
                    Id = 2,
                    UserName = "user",
                    FirstName = "Steven",
                    LastName = "Solberg",
                    Email = "steven@mail.com",
                    Password = "1020",
                    PhoneNumber = null,
                    IsAdmin = false,
                },
                new User()
                {
                    Id = 3,
                    UserName = "user",
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Email = "ivan@mail.com",
                    Password = "3344",
                    PhoneNumber = null,
                    IsAdmin = false,
                }
            };

            modelBuilder.Entity<User>().HasData(users);

            // Seed categories

            List<Category> categories = new List<Category>()
            {
                new Category { Id = 1, Name = "Tuning" },
                new Category { Id = 2, Name = "Engines" },
                new Category { Id = 3, Name = "Suspension" },
                new Category { Id = 4, Name = "Electronics" }
            };

            modelBuilder.Entity<Category>().HasData(categories);

            // Seed posts

            List<Post> posts = new List<Post>()
            {
                new Post
                {
                    Id = 1,
                    CategoryID = 1,
                    UserID = 1,
                    Title = "I got my supra 1200 HP. Here is how i did that...",
                    Content = "Step by step tutorial.",
                    CreateDate = DateTime.Now,
                }
            };

            modelBuilder.Entity<Post>().HasData(posts);

            // Seed comments

            List<Comment> comments = new List<Comment>()
            {
                new Comment()
                {
                    Id = 1,
                    UserID = 1,
                    Content = "Awesome. I will follow your tutorial to tune my supra."
                }
            };

            modelBuilder.Entity<Comment>().HasData(comments);
        }
    }
}
