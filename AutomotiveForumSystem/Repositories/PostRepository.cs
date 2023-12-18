using AutomotiveForumSystem.Data;
using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.PostDtos;
using AutomotiveForumSystem.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveForumSystem.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationContext applicationContext;

        public PostRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public Post Create(Post post, User currentUser)
        {
            post.CreateDate = DateTime.Now;
            currentUser.Posts.Add(post);
            this.applicationContext.Posts.Add(post);
            return post;
        }

        public void Delete(Post post, User currentUser)
        {
            currentUser.Posts.Remove(post);
            this.applicationContext.Posts.Remove(post);
        }

        public IList<Post> GetAllPosts()
        {
            IQueryable<Post> postsToReturn = applicationContext.Posts;
            return postsToReturn.Include(p => p.Category).ToList();
        }

        public IList<Post> GetAll(PostQueryParameters postQueryParameters)
        {
            var postsToReturn = this.applicationContext.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(postQueryParameters.Category))
            {
                postsToReturn = postsToReturn.Where(p => p.Category.Name == postQueryParameters.Category);
            }
            if (!string.IsNullOrEmpty(postQueryParameters.Title))
            {
                postsToReturn = postsToReturn.Where(p => p.Title == postQueryParameters.Title);
            }

            return postsToReturn.Include(p => p.Category).ToList();
        }

        public Post GetPostById(int id)
        {
            return applicationContext.Posts.FirstOrDefault(p => p.Id == id)
                ?? throw new EntityNotFoundException($"Post with ID: {id} not found");
        }

        public IList<Post> GetPostsByUser(int userId, PostQueryParameters postQueryParameters)
        {
            var postsToReturn = applicationContext.Posts.AsQueryable()
                .Where(p=> p.UserID == userId);

            if (!string.IsNullOrEmpty(postQueryParameters.Category))
            {
                postsToReturn = postsToReturn.Where(p => p.Category.Name == postQueryParameters.Category);
            }
            if (!string.IsNullOrEmpty(postQueryParameters.Title))
            {
                postsToReturn = postsToReturn.Where(p => p.Title == postQueryParameters.Title);
            }
            return postsToReturn.Include(p => p.Category).ToList();
        }

        public bool PostExist(string title)
        {
            return applicationContext.Posts.Any(p => p.Title == title);
        }

        public Post Update(int id, Post post)
        {
            var postToUpdate = applicationContext.Posts.FirstOrDefault(p => p.Id == id)
                ?? throw new EntityNotFoundException($"Post with ID: {id} not found");

            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;
            postToUpdate.Category = post.Category;
            applicationContext.SaveChanges();

            return postToUpdate;
        }
    }
}
