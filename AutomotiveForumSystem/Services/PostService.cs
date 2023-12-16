using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Repositories;
using AutomotiveForumSystem.Services.Contracts;

namespace AutomotiveForumSystem.Services
{
    public class PostService : IPostService
    {
        private readonly PostRepository postRepository;
        public PostService(PostRepository postRepository)
        {
            this.postRepository = postRepository;
        }
        public Post Create(Post post, User currentUser)
        {
            if (currentUser.IsBlocked)
            {
                throw new BlockedUserException($"User {currentUser.UserName} is currently blocked");
            }
            postRepository.Create(post, currentUser);
            return post;
        }

        public void Delete(int id, User currentUser)
        {
            var postToDelete = this.postRepository.GetById(id);
            if (!IsPostCreatedByUser(postToDelete, currentUser) && !IsUserAdmin(currentUser))
            {
                throw new AuthorizationException("Not admin or post creator!");
            }
            this.postRepository.Delete(postToDelete, currentUser);
        }

        public IList<PostResponseDto> GetAll()
        {
            return this.postRepository.GetAll();
        }

        public IList<PostResponseDto> GetByCategory(string categoryName)
        {
            return this.postRepository.GetByCategory(categoryName);
        }

        public Post GetById(int id)
        {
            return this.postRepository.GetById(id);
        }

        public IList<PostResponseDto> GetByUser(int userId, PostQueryParameters postQueryParameters)
        {
            return this.postRepository.GetByUser(userId, postQueryParameters);
        }

        public Post Update(int id, Post post, User currentUser)
        {
            var postToUpdate = this.postRepository.GetById(id);
            if (!IsPostCreatedByUser(postToUpdate, currentUser))
            {
                throw new AuthorizationException("Not post creator!");
            }
            return this.postRepository.Update(id, postToUpdate);
        }

        private bool IsPostCreatedByUser(Post post, User currentUser)
        {
            return currentUser.Posts.Any(p => p.Id == post.Id);
        }

        private bool IsUserAdmin(User currentUser)
        {
            return currentUser.Role.Name == "admin";
        }
    }
}
