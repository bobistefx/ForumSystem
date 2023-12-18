using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.PostDtos;

namespace AutomotiveForumSystem.Services.Contracts
{
    public interface IPostService
    {
        IList<Post> GetAllPosts();
        IList<Post> GetAll(PostQueryParameters postQueryParameters);
        IList<Post> GetPostsByUser(int userId, PostQueryParameters postQueryParameters);
        Post GetPostById(int id);
        Post Create(Post post, User currentUser);
        Post Update(int id, Post post, User currentUser);
        void Delete(int id, User currentUser);
    }
}
