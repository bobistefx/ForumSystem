using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Helpers;
using AutomotiveForumSystem.Helpers.Contracts;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.PostDtos;
using AutomotiveForumSystem.Repositories;
using AutomotiveForumSystem.Repositories.Contracts;
using AutomotiveForumSystem.Services.Contracts;

namespace AutomotiveForumSystem.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;

        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }
        public Post Create(Post post, User currentUser)
        {
            if (currentUser.IsBlocked)
            {
                throw new UserBlockedException($"User {currentUser.UserName} is currently blocked");
            }
            this.postRepository.Create(post, currentUser);
            return post;
        }

        public void Delete(int id, User currentUser)
        {
            var postToDelete = this.postRepository.GetPostById(id);
            if (!IsPostCreatedByUser(postToDelete, currentUser) && !IsUserAdmin(currentUser))
            {
                throw new AuthorizationException("Not admin or post creator!");
            }
            this.postRepository.Delete(postToDelete, currentUser);
        }

        public IList<Post> GetAllPosts()
        {
            return this.postRepository.GetAllPosts();
        }

        public IList<Post> GetAll(PostQueryParameters postQueryParameters)
        {
            return this.postRepository.GetAll(postQueryParameters);
        }

        public Post GetPostById(int id)
        {
            return this.postRepository.GetPostById(id);
        }

        public IList<Post> GetPostsByUser(int userId, PostQueryParameters postQueryParameters)
        {
            return this.postRepository.GetPostsByUser(userId, postQueryParameters);
        }

        public Post Update(int id, Post post, User currentUser)
        {
            var postToUpdate = this.postRepository.GetPostById(id);
            if (!IsPostCreatedByUser(postToUpdate, currentUser))
            {
                throw new AuthorizationException("Not post creator!");
            }
            return this.postRepository.Update(id, post);
        }

        private bool IsPostCreatedByUser(Post post, User currentUser)
        {
            return post.UserID == currentUser.Id;
        }

        private bool IsUserAdmin(User currentUser)
        {
            return currentUser.IsAdmin;
        }
    }
}
