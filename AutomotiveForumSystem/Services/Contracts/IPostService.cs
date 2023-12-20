using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.PostDtos;

namespace AutomotiveForumSystem.Services.Contracts
{
    public interface IPostService
    {
        IList<Post> GetAll(PostQueryParameters postQueryParameters);
        IList<Post> GetPostsByUser(int userId, PostQueryParameters postQueryParameters);
        Post GetPostById(int id);
        Post CreatePost(Post post, User currentUser);
        Post Update(int id, Post post, User currentUser);
        void DeletePost(int id, User currentUser);
    }
}
