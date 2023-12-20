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

        public Post CreatePost(Post post, User currentUser)
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

        public void DeletePost(Post post, User currentUser)
        {
            //currentUser.Posts.Remove(post);
            post.IsDeleted = true;
            applicationContext.SaveChanges();
        }

        public IList<Post> GetAll(PostQueryParameters postQueryParameters)
        {
            var postsToReturn = this.applicationContext.Posts
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(postQueryParameters.Category))
            {
                postsToReturn = postsToReturn.Where(p => p.Category.Name == postQueryParameters.Category);
            }
            if (!string.IsNullOrEmpty(postQueryParameters.Title))
            {
                postsToReturn = postsToReturn.Where(p => p.Title == postQueryParameters.Title);
            }

            return postsToReturn.ToList();
        }

        public Post GetPostById(int id)
        {
            return applicationContext.Posts.Include(p => p.Category).FirstOrDefault(p => p.Id == id && !p.IsDeleted)
                ?? throw new EntityNotFoundException($"Post with ID: {id} not found");
        }

        public IList<Post> GetPostsByUser(int userId, PostQueryParameters postQueryParameters)
        {
            var postsToReturn = applicationContext.Posts
                .Where(p => p.UserID == userId && !p.IsDeleted)
                .AsQueryable();

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

        public Post UpdatePost(int id, Post updatedPost)
        {
            var postToUpdate = applicationContext.Posts.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
                ?? throw new EntityNotFoundException($"Post with ID: {id} not found");

            postToUpdate.Title = updatedPost.Title;
            postToUpdate.Content = updatedPost.Content;

            if (postToUpdate.CategoryID != updatedPost.CategoryID)
            {
                var newCategory = applicationContext.Categories.FirstOrDefault(c => c.Id == updatedPost.CategoryID)
                    ?? throw new EntityNotFoundException($"Category with ID {updatedPost.CategoryID} not found");
                postToUpdate.CategoryID = updatedPost.CategoryID;
            }

            applicationContext.SaveChanges();

            return postToUpdate;
        }
    }
}
