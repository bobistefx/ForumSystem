using AutomotiveForumSystem.Data;
using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Repositories.Contracts;

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
            currentUser.Posts.Add(post);
            this.applicationContext.Posts.Add(post);
            return post;
        }

        public void Delete(Post post, User currentUser)
        {
            currentUser.Posts.Remove(post);
            this.applicationContext.Posts.Remove(post);
        }

        public IList<PostResponseDto> GetAll()
        {
            IList<PostResponseDto> result = applicationContext.Posts
                .Select(p => new PostResponseDto()
                {
                    Category = p.Category.Name,
                    Title = p.Title,
                    Content = p.Content,
                    CreateDate = p.CreateDate,
                    Comments = p.Comments,
                    Likes = p.Likes,
                })
                .ToList();
            return result;
        }

        public IList<PostResponseDto> GetByCategory(string categoryName)
        {
            var postsToReturn = this.applicationContext.Posts.AsQueryable()
                .Where(p => p.Category.Name == categoryName);

            IList<PostResponseDto> result = postsToReturn
                .Select(p => new PostResponseDto()
                {
                    Category = p.Category.Name,
                    Title = p.Title,
                    Content = p.Content,
                })
                .ToList();
            return result;
        }

        public Post GetById(int id)
        {
            return applicationContext.Posts.FirstOrDefault(p => p.Id == id)
                ?? throw new EntityNotFoundException($"Post with ID: {id} not found");
        }

        public IList<PostResponseDto> GetByUser(int userId, PostQueryParameters postQueryParameters)
        {
            var user = this.applicationContext.Users.FirstOrDefault(u => u.Id == userId)
                ?? throw new EntityNotFoundException($"User with ID: {userId} doesn't exist");

            var postsToReturn = user.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(postQueryParameters.Category))
            {
                postsToReturn = postsToReturn.Where(p => p.Category.Name == postQueryParameters.Category);
            }
            if (!string.IsNullOrEmpty(postQueryParameters.Title))
            {
                postsToReturn = postsToReturn.Where(p => p.Title == postQueryParameters.Title);
            }

            List<PostResponseDto> result = postsToReturn
                .Select(p => new PostResponseDto()
                {
                    Category = p.Category.Name,
                    Title = p.Title,
                    Content = p.Content,
                    CreateDate = p.CreateDate,
                    Comments = p.Comments,
                    Likes = p.Likes
                })
                .ToList();
            return result;
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
