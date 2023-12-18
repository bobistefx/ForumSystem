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
            this.applicationContext.Posts.Add(post);
            currentUser.Posts.Add(post);
            applicationContext.SaveChanges();

            var createdPost = applicationContext.Posts
            .Include(p => p.Category)
            .FirstOrDefault(p => p.Id == post.Id);

            return createdPost;
        }

        public void Delete(Post post, User currentUser)
        {
            //currentUser.Posts.Remove(post);
            post.IsDeleted = true;
            applicationContext.SaveChanges();
        }

        public IList<Post> GetAllPosts()
        {
            IQueryable<Post> postsToReturn = applicationContext.Posts.Where(p => !p.IsDeleted);
            return postsToReturn.Include(p => p.Category).ToList();
        }

        public IList<Post> GetAll(PostQueryParameters postQueryParameters)
        {
            var postsToReturn = this.applicationContext.Posts.Where(p => !p.IsDeleted).AsQueryable();

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
            return applicationContext.Posts.Include(p => p.Category).FirstOrDefault(p => p.Id == id && !p.IsDeleted)
                ?? throw new EntityNotFoundException($"Post with ID: {id} not found");
        }

        public IList<Post> GetPostsByUser(int userId, PostQueryParameters postQueryParameters)
        {
            var postsToReturn = applicationContext.Posts.AsQueryable()
                .Where(p=> p.UserID == userId && !p.IsDeleted);

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

        public Post Update(int id, Post post)
        {
            var postToUpdate = applicationContext.Posts.Include(p => p.Category).FirstOrDefault(p => p.Id == id && !p.IsDeleted)
                ?? throw new EntityNotFoundException($"Post with ID: {id} not found");

            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;
            postToUpdate.CategoryID = post.CategoryID;

            applicationContext.Entry(postToUpdate).Reference(p => p.Category).Load();

            applicationContext.SaveChanges();

            return postToUpdate;
        }
    }
}
