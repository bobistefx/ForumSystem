using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.PostDtos;

namespace AutomotiveForumSystem.Repositories.Contracts
{
    public interface IPostRepository
    {
        IList<Post> GetAllPosts();
        IList<Post> GetAll(PostQueryParameters postQueryParameters);
        IList<Post> GetPostsByUser(int userId, PostQueryParameters postQueryParameters);
        Post GetPostById(int id);
        Post Create(Post post, User currentUser);
        Post Update(int id, Post post);
        void Delete(Post post, User currentUser);
    }
}
